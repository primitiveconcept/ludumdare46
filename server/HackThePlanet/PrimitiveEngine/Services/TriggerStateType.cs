﻿namespace PrimitiveEngine
{
    /// <summary>
    /// Enumeration TriggerStateType.
    /// </summary>
    public enum TriggerStateType : long
    {
        ValueAdded   = 0x00001,
	    ValueRemoved = 0x00010,
        ValueChanged = 0x00100,
        TriggerAdded = 0x01000
    }
}