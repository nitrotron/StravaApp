﻿#region Copyright (C) 2014 Sascha Simon

//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  Visit the official homepage at http://www.sascha-simon.com

#endregion

using System;

namespace com.strava.v3.api.Streams
{
    /// <summary>
    /// Specifies which information about a segment is being received.
    /// </summary>
    [Flags]
    public enum SegmentStreamType
    {
        /// <summary>
        /// Segment coordinate information is received.
        /// </summary>
        LatLng = 1,
        /// <summary>
        /// Segment distance information is received.
        /// </summary>
        Distance = 2,
        /// <summary>
        /// Segment altitude information is received.
        /// </summary>
        Altitude = 4,
        /// <summary>
        /// Segment time information is received.
        /// </summary>
        Time = 8
    }
}