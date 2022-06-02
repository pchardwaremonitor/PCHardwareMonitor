﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) PCHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

namespace PCHardwareMonitor.Hardware
{
    /// <summary>
    /// Collection of identifiers representing the purpose of the hardware.
    /// </summary>
    public enum HardwareType
    {
        Motherboard,
        SuperIO,
        Cpu,
        Memory,
        GpuNvidia,
        GpuAmd,
        GpuIntel,
        Storage,
        Network,
        Cooler,
        EmbeddedController,
        Psu,
        Battery
    }
}
