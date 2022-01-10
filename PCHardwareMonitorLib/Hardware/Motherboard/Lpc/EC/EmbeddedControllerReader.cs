﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) PCHardwareMonitor and Contributors.
// All Rights Reserved.

namespace PCHardwareMonitor.Hardware.Motherboard.Lpc.EC
{
    public delegate float EmbeddedControllerReader(IEmbeddedControllerIO ecIO, ushort register);
}
