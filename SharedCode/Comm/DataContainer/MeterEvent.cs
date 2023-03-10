namespace SharedCode.Comm.DataContainer
{
    public enum MeterEvent : ushort
    {
        ImbalanceVolt = 1,
        PhaseSequence = 2,
        ReversePolarity = 3,
        Phasefail = 4,
        UnderVolt = 5,
        OverVolt = 6,
        OverCurrent = 7,
        HighNeutralCurrent = 8,
        OverLoad = 9,
        ReverseEnergy = 10,
        TamperEnergy = 11,
        CTFail = 12,
        PTFail = 13,
        OpticalportLogin = 14,
        PowerFail = 15,
        PowerFailEnd = 16,
        OneWireTampering = 17,
        MeterOnLoad = 18,
        MeterOnLoadEnd = 19,
        UnderVoltEnd = 20,
        MDIExceed = 21,
        SystemReset = 22,
        SystemProblems = 23,
        MDIReset = 24,
        Parameters = 25,
        PasswordChange = 26,
        CustomerCode = 27,
        TimeChange = 28,
        WindowSequenseChange = 29,
        OverVoltEnd = 30,
        BillRegisterOverFlow = 31,
        ParamError = 32,
        PowerFactorChange = 33,
        BattreyLow = 34,
        DoorOpen = 35,
        ShortTimePowerFail = 36,
        RecordRecoverd = 37,
        TimeBaseEvent_1 = 38,
        TimeBaseEvent_2 = 39,
        ContactorStatusOn = 40,
        ContactorStatusOff = 41,
        ShortTimePowerFailEnd = 42,
        ReverseEnergyEnd = 43,
        TamperEnergyEnd = 44,
        OverLoadEnd = 45,
        MDIOccurance = 46,
        BillRegisterError = 47,
        PhasePhail_End = 48,
        MagneticFeild_End = 49,
        CTFail_End = 50,
        PTFail_End = 51,
        Software_Logout = 52,
        Reserved_07 = 53,
        Generator_Start = 54, // Reserved_06 = 54,
        Generator_End = 55,  // Reserved_05
        Reserved_04 = 56,
        Reserved_03 = 57,
        Reserved_02 = 58,
        Reserved_01 = 59,
        Reserved_00 = 60
    }
}

