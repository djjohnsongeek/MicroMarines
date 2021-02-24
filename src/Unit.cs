using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src
{
    public enum Orientation
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3,

        /*
        South = 0,
        SouthEast = 1,
        East = 2,
        NorthEast = 3,
        North = 4,
        NorthWest = 5,
        West = 6,
        SouthWest = 7,
        */
    }

    public enum AnimationType
    {
        Idle = 0,
        Walking = 4,
    }   

    class Unit
    {
        // skin/animation
        public Orientation CurrOrientation;
        public AnimationType CurrAnimType;
        public Animation Animation;
        public Texture2D SpriteSheet;
        public Rectangle[,] Frames;

        // unit attributes
        private float speed = 220f;
        private int width = 32;
        private int height = 32;

        // position related
        public Vector2 Position;
        public Queue<Vector2> Waypoints;
        private Vector2? currentWaypoint;
        private Vector2 velocity;
        private Vector2 baseDistance;
        private float travelDistance;

        // state
        public states.unit.UnitStateMachine State;
        private ushort id;
        public ushort Id { get; }

        // commands
        public bool Selected = false;
        public bool ReadyForCommand = true;

        public Unit(ContentManager Content, string spriteSheetName, ushort unitId)
        {
            // texture
            SpriteSheet = Content.Load<Texture2D>(spriteSheetName);
            Frames = prepareRects(32, 32, 256, 256);
            CurrOrientation = Orientation.South;
            CurrAnimType = AnimationType.Idle;
            Animation = new Animation(0.15f, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, true);

            // state
            State = new states.unit.UnitStateMachine(this);
            State.Change("idle");
            id = unitId;

            // Position and Movement
            Position = new Vector2(Camera.Width / 2, Camera.Height / 2);
            Waypoints = new Queue<Vector2>();
            currentWaypoint = null;
            velocity = new Vector2(0, 0);
            baseDistance = new Vector2(0, 0);
            travelDistance = 0f;
        }

        // [ MAIN GAME LOOP ] //
        public void Update(GameTime gameTime)
        {
            State.CurrentState.Update(gameTime);
            // Util.Print($"UNIT: {Position.X}, {Position.Y}");
        }
        public void Draw(SpriteBatch sBatch)
        {
            State.CurrentState.Draw(sBatch);
        }

        // [ PUBIC METHODS ] //
        public void UpdateSelection()
        {
            if (Util.MouseCollides(this) && Input.mState.LeftButton == ButtonState.Pressed)
            {
               Selected = true;
            }
            else
            {
                if (Input.mState.LeftButton == ButtonState.Pressed)
                {
                   Selected = false;
                }
            }
        }

        public bool ReceivesMoveCommand()
        {
            return (Input.mState.RightButton == ButtonState.Pressed) &&
                   (Input.prevMState.RightButton != ButtonState.Pressed) &&
                    !Input.kState.IsKeyDown(Keys.LeftShift) &&
                    Selected &&
                    ReadyForCommand;
        }
        public bool ReceivesQueueCommand()
        {
            return (Input.mState.RightButton == ButtonState.Pressed) &&
                   (Input.prevMState.RightButton != ButtonState.Pressed) &&
                    Input.kState.IsKeyDown(Keys.LeftShift) &&
                    Selected &&
                    ReadyForCommand;
        }

        // TODO NEED TO RENAME
        public void OverWriteWaypoints()
        {
            Vector2 newWaypoint = Input.GetMouseWorldPos();

            if (isValidWaypoint(newWaypoint))
            {
                Waypoints.Clear();
                currentWaypoint = null;
                travelDistance = 0;
                Waypoints.Enqueue(newWaypoint);
            }
        }
        public void GetNextWaypoint()
        {
            if (currentWaypoint == null && Waypoints.Count > 0)
            {
                currentWaypoint = Waypoints.Dequeue();
            }
        }
        public void FollowWaypoints(float dt)
        {
            if (currentWaypoint != null)
            {
                updateVelocity(dt);
                move();
                updateOrientation();
            }
            else if (Waypoints.Count == 0)
            {
                State.Change("idle");
            }
        }
        public Vector2 GetPosition()
        {
            return Position;
        }
        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }
        public bool GetSelectionState()
        {
            return Selected;
        }

        public Rectangle GetRectangle()
        {
            Point originPos= new Point((int)Position.X - (width / 2), (int)Position.Y - (height / 2));
            return new Rectangle(originPos, new Point(width, height));
        }

        // [  PRIVATE METHODS ] //
        private Rectangle[,] prepareRects(int xStep, int yStep, int width, int height)
        {
            Rectangle[,] frames = new Rectangle[8, 8];
            int xIndex = 0;  int yIndex = 0;

            for (int y = 0; y < width; y += yStep)
            {
                for (int x = 0; x < height; x += xStep)
                {
                    frames[yIndex, xIndex].X = x;
                    frames[yIndex, xIndex].Y = y;
                    frames[yIndex, xIndex].Width = xStep;
                    frames[yIndex, xIndex].Height = yStep;

                    xIndex ++;
                }

                xIndex = 0;
                yIndex ++;
            }

            return frames;
        }
        private void updateOrientation()
        {
            float dotProduct = Math.Abs(Vector2.Dot(baseDistance, Vector2.UnitX));

            // Unit not moving, leave Orientation as is
            if (dotProduct == 0)
            {

            }
            // Unit moving 'almost' directly up or down
            else if (dotProduct < .20f)
            {
                if (baseDistance.Y < 0)
                {
                    CurrOrientation = Orientation.North;
                }
                else
                {
                    CurrOrientation = Orientation.South;
                }
            }
            // Unit moving diagonally
            else if (dotProduct > .20f && dotProduct < .80f)
            {
                if (baseDistance.X > 0)
                {
                    if (baseDistance.Y > 0)
                    {
                        // CurrOrientation = Orientation.SouthEast;
                        CurrOrientation = Orientation.South;
                    }
                    else
                    {
                        // CurrOrientation = Orientation.NorthEast;
                        CurrOrientation = Orientation.North;
                    }
                }
                else
                {
                    if (baseDistance.X < 0)
                    {
                        if (baseDistance.Y > 0)
                        {
                            // CurrOrientation = Orientation.SouthWest;
                            CurrOrientation = Orientation.South;
                        }
                        else
                        {
                            // CurrOrientation = Orientation.NorthWest;
                            CurrOrientation = Orientation.North;
                        }
                    }
                }
            }
            // Unit moving 'almost' directly left or right
            else if (dotProduct > .80f)
            {
                if (baseDistance.X > 0)
                {
                    CurrOrientation = Orientation.East;
                }
                else
                {
                    CurrOrientation = Orientation.West;
                }
            }
        }
        private void updateVelocity(float dt)
        {
            if (travelDistance == 0)
            {
                // only need to normalize once per waypoint
                baseDistance = currentWaypoint.Value - Position;
                travelDistance = Util.GetHypotenuse(baseDistance);
                baseDistance.Normalize();
            }

            // velocity should be seprate from normalized vector
            velocity.X = baseDistance.X * speed * dt;
            velocity.Y = baseDistance.Y * speed * dt;

            // Util.Print($"dx: {velocity.X} dy: {velocity.Y}");
        }
        private void move()
        {
            Position += velocity;
            travelDistance -= Util.GetHypotenuse(velocity);

            if (hasArrived())
            {
                markArrived();
            }
        }
        private bool hasArrived()
        {
            if (currentWaypoint == null)
            {
                return true;
            }

            return travelDistance <= 0;
        }
        private void markArrived()
        {
            // Util.Print("ARRIVED");
            Position = currentWaypoint.Value;
            currentWaypoint = null;
            velocity.X = 0;
            velocity.Y = 0;
            travelDistance = 0;
            baseDistance.X = 0;
            baseDistance.Y = 0;
        }

        private bool isValidWaypoint(Vector2 newWaypoint)
        {
            bool waypointEqualsUnitPos = newWaypoint != Camera.GetWorldLocation(Position);
            if (Waypoints.Count == 0)
            {
                return waypointEqualsUnitPos;
            }


            return waypointEqualsUnitPos && newWaypoint != Waypoints.Peek();
        }
    }
}

