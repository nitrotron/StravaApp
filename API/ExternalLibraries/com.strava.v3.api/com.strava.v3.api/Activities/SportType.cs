#region Copyright (C) 2014 Sascha Simon

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

namespace com.strava.v3.api.Activities
{
    /// <summary>
    /// The type of an activity.
    /// </summary>
    public enum SportType
    {
        /// <summary>AlpineSki</summary>
        AlpineSki,
        /// <summary>BackcountrySki</summary>
        BackcountrySki,
        /// <summary>Badminton</summary>
        Badminton,
        /// <summary>Canoeing</summary>
        Canoeing,
        /// <summary>Crossfit</summary>
        Crossfit,
        /// <summary>EBikeRide</summary>
        EBikeRide,
        /// <summary>Elliptical</summary>
        Elliptical,
        /// <summary>EMountainBikeRide</summary>
        EMountainBikeRide,
        /// <summary>Golf</summary>
        Golf,
        /// <summary>GravelRide</summary>
        GravelRide,
        /// <summary>Handcycle</summary>
        Handcycle,
        /// <summary>HighIntensityIntervalTraining</summary>
        HighIntensityIntervalTraining,
        /// <summary>Hike</summary>
        Hike,
        /// <summary>IceSkate</summary>
        IceSkate,
        /// <summary>InlineSkate</summary>
        InlineSkate,
        /// <summary>Kayaking</summary>
        Kayaking,
        /// <summary>Kitesurf</summary>
        Kitesurf,
        /// <summary>MountainBikeRide</summary>
        MountainBikeRide,
        /// <summary>NordicSki</summary>
        NordicSki,
        /// <summary>Pickleball</summary>
        Pickleball,
        /// <summary>Pilates</summary>
        Pilates,
        /// <summary>Racquetball</summary>
        Racquetball,
        /// <summary>Ride</summary>
        Ride,
        /// <summary>RockClimbing</summary>
        RockClimbing,
        /// <summary>RollerSki</summary>
        RollerSki,
        /// <summary>Rowing</summary>
        Rowing,
        /// <summary>Run</summary>
        Run,
        /// <summary>Sail</summary>
        Sail,
        /// <summary>Skateboard</summary>
        Skateboard,
        /// <summary>Snowboard</summary>
        Snowboard,
        /// <summary>Snowshoe</summary>
        Snowshoe,
        /// <summary>Soccer</summary>
        Soccer,
        /// <summary>Squash</summary>
        Squash,
        /// <summary>StairStepper</summary>
        StairStepper,
        /// <summary>StandUpPaddling</summary>
        StandUpPaddling,
        /// <summary>Surfing</summary>
        Surfing,
        /// <summary>Swim</summary>
        Swim,
        /// <summary>TableTennis</summary>
        TableTennis,
        /// <summary>Tennis</summary>
        Tennis,
        /// <summary>TrailRun</summary>
        TrailRun,
        /// <summary>Velomobile</summary>
        Velomobile,
        /// <summary>VirtualRide</summary>
        VirtualRide,
        /// <summary>VirtualRow</summary>
        VirtualRow,
        /// <summary>VirtualRun</summary>
        VirtualRun,
        /// <summary>Walk</summary>
        Walk,
        /// <summary>WeightTraining</summary>
        WeightTraining,
        /// <summary>Wheelchair</summary>
        Wheelchair,
        /// <summary>Windsurf</summary>
        Windsurf,
        /// <summary>Workout</summary>
        Workout,
        /// <summary>Yoga</summary>
        Yoga,
        /// <summary>
        /// Unknown - only returned if the activity type cannot be parsed (could be that a new type of activity has been introduced that the API doesnt yet support)
        /// </summary>
        Unknown
    }
}