using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Micro_Marine.src
{
    public static class Camera
    {
        private static float zoom = 1.0f;
        public static Matrix Transform;
        public static Vector2 Position = new Vector2(1920 / 2, 1080 / 2);
        public static int Width = 1920;
        public static int Height = 1080;
        private static float rotation = 0.0f;
        private static float speed = 399.0f;
        private static int edgeBuffer = 45;

        public static float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public static float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public static void Move(Vector2 distance)
        {
            Position += distance;
        }

        public static void Update(float dt)
        {
            Move(getVelocity(dt));

           // Util.Print($"CAMERA: {Position.X}, {Position.Y}");
        }

        public static Matrix GetTransformation()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(Width * 0.5f, Height * 0.5f, 0));

            return Transform;
        }

        public static Vector2 GetWorldLocation(Vector2 screenLocation)
        {
            return Vector2.Transform(screenLocation, Matrix.Invert(GetTransformation()));
        }

        private static Vector2 getVelocity(float dt)
        {
            float dx = 0f;
            float dy = 0f;

            // left scroll
            if (Input.mState.X < edgeBuffer)
            {
                dx = -(speed * dt);
            }

            // right scroll
            if (Input.mState.X > Width - edgeBuffer)
            {
                dx = speed * dt;
            }

            // down scroll
            if (Input.mState.Y > Height - edgeBuffer)
            {
                dy = speed * dt;
            }

            // scroll up
            if (Input.mState.Y < edgeBuffer)
            {
                dy = -(speed * dt);
            }

            return new Vector2(dx, dy);
        }


        // go from world to screen space
        // Vector2.Transform(mouseLocation, Camera.TransformMatrix);
    }
}
