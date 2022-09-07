// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) PCHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

using System;
using System.Runtime.InteropServices;
using System.Windows;
using PCHardwareMonitor.Interop;

namespace PCHardwareMonitor.Hardware.Screen
{
    [StructLayout(LayoutKind.Sequential)]
    struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    internal sealed class Screen : Hardware
    {
        private readonly Sensor _widthResolution;
        private readonly Sensor _heightResolution;
        private readonly Sensor _refreshRate;

        public Screen(string name, ISettings settings) : base(name, new Identifier("screen"), settings)
        {
            _refreshRate = new Sensor("Refresh Rate", 0, SensorType.Screen, this, settings);
            ActivateSensor(_refreshRate);

            _heightResolution = new Sensor("Height Resolution", 0, SensorType.Screen, this, settings);
            ActivateSensor(_heightResolution);

            _widthResolution = new Sensor("Width Resolution", 0, SensorType.Screen, this, settings);
            ActivateSensor(_widthResolution);
        }

        public override HardwareType HardwareType
        {
            get { return HardwareType.Screen; }
        }

        [DllImport("user32.dll")]
        static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        public override void Update()
        {
            const int ENUM_CURRENT_SETTINGS = -1;

            DEVMODE devMode = default;
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);

            _widthResolution.Value = devMode.dmPelsWidth;
            _heightResolution.Value = devMode.dmPelsHeight;
            _refreshRate.Value = devMode.dmDisplayFrequency;
        }
    }
}
