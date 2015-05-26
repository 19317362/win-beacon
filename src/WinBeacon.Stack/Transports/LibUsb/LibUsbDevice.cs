﻿/*
 * Copyright 2015 Huysentruit Wouter
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.ObjectModel;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;

namespace WinBeacon.Stack.Transports.LibUsb
{
    /// <summary>
    /// LibUsbDotNet.UsbDevice wrapper that implements ILibUsbDevice.
    /// </summary>
    internal class LibUsbDevice : ILibUsbDevice
    {
        private UsbDevice usbDevice;

        public LibUsbDevice(int vid, int pid)
        {
            usbDevice = UsbDevice.OpenUsbDevice(new UsbDeviceFinder(vid, pid));
        }

        ~LibUsbDevice()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (usbDevice != null)
                usbDevice.Close();
            usbDevice = null;
        }

        public ReadOnlyCollection<UsbConfigInfo> Configs
        {
            get { return usbDevice.Configs; }
        }

        public UsbEndpointReader OpenEndpointReader(ReadEndpointID readEndpointID)
        {
            return usbDevice.OpenEndpointReader(readEndpointID);
        }

        public UsbEndpointWriter OpenEndpointWriter(WriteEndpointID writeEndpointID)
        {
            return usbDevice.OpenEndpointWriter(writeEndpointID);
        }

        public bool ControlTransfer(ref UsbSetupPacket setupPacket, object buffer, int bufferLength, out int lengthTransferred)
        {
            return usbDevice.ControlTransfer(ref setupPacket, buffer, bufferLength, out lengthTransferred);
        }
    }
}