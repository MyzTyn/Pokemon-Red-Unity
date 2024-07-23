using UnityEngine;

public static class IntExtensions
{
    /*
    The first two should either be improved or just removed and have number formatting
    for different things be done directly (just use string.Format or ToString instead directly
    rather than use these functions)
    */
    public static string ZeroFormat(this int input, string zeroformat)
    {
        switch(zeroformat)
        {
            case "0x":
                return (input > 9 ? "" : "0") + input;
            case "00x":
                return (input > 99 ? "" : input > 9 ? "0" : "00") + input;
            case "000x":
                return (input > 999 ? "" : input > 99 ? "0" : input > 9 ? "00" : "000") + input;
        }
        throw new UnityException("Incorrect format");

    }
    
    public static string SpaceFormat(this int input, int format)
    {
        switch(format)
        {
            case 2:
                return (input < 10 ? " " : "") + input;
            case 3:
                return (input < 10 ? "  " : input < 100 ? " ": "") + input;
        }
        throw new UnityException("Incorrect format");

    }
    
    //underflow function for the exp bug
    public static int UnderflowUInt24(this int input)
    { 
        if(input < 0)
        {
            input += (int)Mathf.Pow(2,24);
        }
        else if (input >= Mathf.Pow(2,24)) input %= (int)Mathf.Pow(2,24);
        return input;
    }
}