using SharpDX.DirectInput;
using System;

namespace RetroVirtualCockpit.InputTester
{
    public class JoystickStateComparer
    {
        private const int MinimumAxisValueChange = 10000;

        public static void Compare(Joystick stick, JoystickState currentState, JoystickState previousState)
        {
            CompareAxisValues(stick, currentState, previousState);
            CompareButtons(stick, currentState, previousState);
            CompareSliders(stick, currentState.Sliders, previousState.Sliders, "Slider");
            CompareSliders(stick, currentState.ForceSliders, previousState.ForceSliders, "Force Slider");
            CompareSliders(stick, currentState.VelocitySliders, previousState.VelocitySliders, "Velocity Slider");
            ComparePovControllers(stick, currentState, previousState);
        }

        private static void ComparePovControllers(Joystick stick, JoystickState currentState, JoystickState previousState)
        {
            for (var i = 0; i < currentState.PointOfViewControllers.Length; i++)
            {
                ComparePovController(stick, currentState, previousState, i);
            }
        }

        private static void ComparePovController(Joystick stick, JoystickState currentState, JoystickState previousState, int i)
        {
            if (currentState.PointOfViewControllers[i] != previousState.PointOfViewControllers[i])
            {
                Console.WriteLine($"{stick.Information.InstanceName} POV {i} : {currentState.PointOfViewControllers[i]}");
            }
        }

        private static void CompareSliders(Joystick stick, int[] currentSliderState, int[] previousSliderState, string sliderType)
        {
            for (var i = 0; i < currentSliderState.Length; i++)
            {
                CompareSlider(stick, currentSliderState[i], previousSliderState[i], i, sliderType);
            }
        }

        private static void CompareSlider(Joystick stick, int currentValue, int previousValue, int i, string sliderType)
        {
            if (Math.Abs(currentValue - previousValue) > MinimumAxisValueChange)
            {
                Console.WriteLine($"{stick.Information.InstanceName} {sliderType} {i}: {currentValue}");
            }
        }

        private static void CompareButtons(Joystick stick, JoystickState currentState, JoystickState previousState)
        {
            for(var i = 0; i < currentState.Buttons.Length; i++)
            {
                CompareButton(stick, currentState, previousState, i);
            }
        }

        private static void CompareButton(Joystick stick, JoystickState currentState, JoystickState previousState, int i)
        {
            if (currentState.Buttons[i] != previousState.Buttons[i])
            {
                Console.WriteLine($"{stick.Information.InstanceName} Button {i} : {currentState.Buttons[i]}");
            }
        }

        private static void CompareAxisValues(Joystick stick, JoystickState currentState, JoystickState previousState)
        {
            CompareAxisValue(stick, "X", "Axis", currentState.X, previousState.X);
            CompareAxisValue(stick, "Y", "Axis", currentState.Y, previousState.Y);
            CompareAxisValue(stick, "Z", "Axis", currentState.Z, previousState.Z);

            CompareAxisValue(stick, "X", "Rotation", currentState.RotationX, previousState.RotationX);
            CompareAxisValue(stick, "Y", "Rotation", currentState.RotationY, previousState.RotationY);
            CompareAxisValue(stick, "Z", "Rotation", currentState.RotationZ, previousState.RotationZ);

            CompareAxisValue(stick, "X", "Acceleration", currentState.AccelerationX, previousState.AccelerationX);
            CompareAxisValue(stick, "Y", "Acceleration", currentState.AccelerationY, previousState.AccelerationY);
            CompareAxisValue(stick, "Z", "Acceleration", currentState.AccelerationZ, previousState.AccelerationZ);

            CompareAxisValue(stick, "X", "AngularAcceleration", currentState.AngularAccelerationX, previousState.AngularAccelerationX);
            CompareAxisValue(stick, "Y", "AngularAcceleration", currentState.AngularAccelerationY, previousState.AngularAccelerationY);
            CompareAxisValue(stick, "Z", "AngularAcceleration", currentState.AngularAccelerationZ, previousState.AngularAccelerationZ);

            CompareAxisValue(stick, "X", "AngularVelocity", currentState.AngularVelocityX, previousState.AngularVelocityX);
            CompareAxisValue(stick, "Y", "AngularVelocity", currentState.AngularVelocityY, previousState.AngularVelocityY);
            CompareAxisValue(stick, "Z", "AngularVelocity", currentState.AngularVelocityZ, previousState.AngularVelocityZ);

            CompareAxisValue(stick, "X", "Torque", currentState.TorqueX, previousState.TorqueX);
            CompareAxisValue(stick, "Y", "Torque", currentState.TorqueY, previousState.TorqueY);
            CompareAxisValue(stick, "Z", "Torque", currentState.TorqueZ, previousState.TorqueZ);

            CompareAxisValue(stick, "X", "Force", currentState.ForceX, previousState.ForceX);
            CompareAxisValue(stick, "Y", "Force", currentState.ForceY, previousState.ForceY);
            CompareAxisValue(stick, "Z", "Force", currentState.ForceZ, previousState.ForceZ);

            CompareAxisValue(stick, "X", "Velocity", currentState.VelocityX, previousState.VelocityX);
            CompareAxisValue(stick, "Y", "Velocity", currentState.VelocityY, previousState.VelocityY);
            CompareAxisValue(stick, "Z", "Velocity", currentState.VelocityZ, previousState.VelocityZ);
        }

        private static void CompareAxisValue(Joystick stick, string name, string property, int currentValue, int previousValue)
        {
            if (Math.Abs(currentValue - previousValue) > MinimumAxisValueChange)
            {
                Console.WriteLine($"{stick.Information.InstanceName} {name} {property}: {currentValue}");
            }
        }
    }
}
