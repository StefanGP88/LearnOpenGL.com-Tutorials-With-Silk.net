using Silk.NET.GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public static class Camera3D
    {
        private static Glfw _glfw;
        private static unsafe WindowHandle* _window;

        private static float fieldOfView = 90.0f;
        private static float nearPlaneDistance = 0.1f;
        private static float farPlaneDistance = 1000.0f;

        private static float Speed = 0.05f;
        public static Vector3 Position = new Vector3(0.0f);

        private static float mouseYaw = -90.0f;
        private static float mousePitch = 0.0f;
        private static float mouseSensivity = 0.1f;
        private static Vector3 LookDirection = new Vector3(0, 0, -1f);
        private static Vector3 UpVector = new Vector3(0, 1, 0);

        private static float lastMousePositionX = 400.0f;
        private static float lastMousePositionY = 300.0f;

        public static Matrix4x4 LookAtMatrix => CalcualteLookAtMatrix();
        public static Matrix4x4 ProjectionMatrix => CalcualteProjectionMatrix();

        static unsafe Camera3D()
        {
            _window = Factory.GetWindow();
            _glfw = Factory.GetGlfw();
            SetCallBacks();
        }

        private static Matrix4x4 CalcualteLookAtMatrix()
        {
            var camtaget = Position + LookDirection;
            return Matrix4x4.CreateLookAt(Position, camtaget, UpVector);
        }
        private static Matrix4x4 CalcualteProjectionMatrix()
        {
            var fieldOfViewAsRadians = ConvertToRadians(fieldOfView);
            var aspectRatio = Settings.View.AspectRatio;
            return Matrix4x4.CreatePerspectiveFieldOfView(fieldOfViewAsRadians, aspectRatio, nearPlaneDistance, farPlaneDistance);
        }

        private static unsafe void SetCallBacks()
        {
            _glfw.SetCursorPosCallback(_window, MouseCallback);
            _glfw.SetKeyCallback(_window, KeyCallback);
            _glfw.SetScrollCallback(_window, MouseWheelCallBack);
        }

        private static unsafe void MouseWheelCallBack(WindowHandle* window, double xoffset, double yoffset)
        {
            fieldOfView -= (float)yoffset;
            if (fieldOfView < 1.0f)
                fieldOfView = 1.0f;
            if (fieldOfView > 179.0f)
                fieldOfView = 179.0f;
            GLDebug.Print($"Field of view: {fieldOfView}");
        }

        private static unsafe void KeyCallback(WindowHandle* window, Keys key, int scanCode, InputAction action, KeyModifiers mods)
        {
            GLDebug.Print(key + " pressed");
            if (action == InputAction.Release) return;

            switch (key)//move camera direction
            {
                case Keys.W: //forward
                    Position += Speed * LookDirection;
                    break;
                case Keys.S://backward
                    Position -= Speed * LookDirection;
                    break;
                case Keys.D://rigt
                    Position += Vector3.Normalize(Vector3.Cross(LookDirection, UpVector));
                    break;
                case Keys.A://left
                    Position -= Vector3.Normalize(Vector3.Cross(LookDirection, UpVector));
                    break;
                case Keys.KeypadAdd:
                    Speed += 0.05f;
                    GLDebug.Print(Speed.ToString());
                    break;
                case Keys.KeypadSubtract:
                    Speed -= 0.05f;
                    GLDebug.Print(Speed.ToString());
                    break;
                case Keys.Space://up
                    Position += Vector3.Normalize(Vector3.UnitY) * Speed;
                    break;
                case Keys.ShiftLeft: //down
                case Keys.ShiftRight:
                    Position -= Vector3.Normalize(Vector3.UnitY) * Speed;
                    break;
            }

            // view distance
            if (mods == KeyModifiers.Alt)
            {
                switch (key)
                {
                    case Keys.Up:
                        nearPlaneDistance += 0.1f;
                        break;
                    case Keys.Down:
                        nearPlaneDistance -= 0.1f;
                        break;
                }
                GLDebug.Print($"nearPlaneDistance: {nearPlaneDistance}");

            }
            else if (mods == KeyModifiers.Control)
            {
                switch (key)
                {
                    case Keys.Up:
                        farPlaneDistance += 1.0f;
                        break;
                    case Keys.Down:
                        farPlaneDistance -= 1.0f;
                        break;
                }
                GLDebug.Print($"farPlaneDistance: {farPlaneDistance}");
            }
            if (nearPlaneDistance < 0.01f)
            {
                nearPlaneDistance = 0.01f;
            }
            if (farPlaneDistance <= nearPlaneDistance)
            {
                nearPlaneDistance = farPlaneDistance - 0.1f;
            }

            if (key == Keys.Escape && action == InputAction.Press)
                _glfw.SetWindowShouldClose(window, true);

        }

        private static unsafe void MouseCallback(WindowHandle* window, double newMousePositionX, double newMousePositionY)
        {
            float xpos = (float)newMousePositionX;
            float ypos = (float)newMousePositionY;

            float xoffset = xpos - lastMousePositionX;
            float yoffset = lastMousePositionY - ypos; // reversed since y-coordinates range from bottom to top
            lastMousePositionX = xpos;
            lastMousePositionY = ypos;


            xoffset *= mouseSensivity;
            yoffset *= mouseSensivity;

            UpdateCameraDirection(xoffset, yoffset);
        }

        private static void UpdateCameraDirection(float xoffset, float yoffset)
        {
            mouseYaw += xoffset;
            mousePitch += yoffset;

            if (mousePitch > 89.9f)
                mousePitch = 89.9f;
            if (mousePitch < -89.9f)
                mousePitch = -89.9f;

            var pitchAsRadians = ConvertToRadians(mousePitch);
            var yawAsRadians = ConvertToRadians(mouseYaw);

            var newX = MathF.Cos(yawAsRadians) * MathF.Cos(pitchAsRadians);
            var newY = MathF.Sin(pitchAsRadians);
            var newZ = MathF.Sin(yawAsRadians) * MathF.Cos(pitchAsRadians);

            LookDirection = Vector3.Normalize(new Vector3(newX, newY, newZ));
        }

        private static float ConvertToRadians(float degree)
        {
            return degree * (MathF.PI / 180);
        }
    }
}
