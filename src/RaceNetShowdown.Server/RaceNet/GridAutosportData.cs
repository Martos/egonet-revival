using System;
using System.Collections.Generic;

public static class GridAutosportData
{
    public enum GridAutosportDisciplineID
    {
        Touring = 2,
        Endurance = 3,
        Openwheel = 4,
        Tuner = 5,
        Street = 6
    }

    public enum GridAutosportVehicleClassID
    {
        SuperModified = 47,
        CatCTouring = 48,
        CuBmwE30M3 = 53,
        ClassicMuscle = 54,
        HotHatch = 56,
        Muscle = 59,
        Jdm = 60,
        CuLightweightCup = 63,
        Supercar = 64,
        C2Drift = 65,
        CatBTouring = 69,
        CatATouring = 70,
        SuperUtes = 71,
        SuperTourers = 72,
        CuClassicMiniCup = 73,
        ClassicTouringCar = 74,
        CuMiniCup = 75,
        EnduranceGtGroup2 = 76,
        EnduranceGtGroup1 = 77,
        EnduranceGtUltimate = 78,
        CuShelbyCup = 82,
        CuFordGt40Cup = 83,
        CuMazda787bCup = 84,
        FormulaC = 86,
        FormulaB = 87,
        FormulaA = 88,
        CuCaterhamLolaCup = 89,
        CuArielAtomV8Cup = 90,
        CuSuperLightweightCup = 91,
        Modified = 92,
        C1Drift = 93,
        CuNissanSkylineCup = 94,
        CuHondaNsxCup = 95,
        Coupe = 96,
        Performance = 97,
        Gt = 98,
        Hypercar = 99,
        CuLanciaDeltaCup = 100,
        CuAlfaRomeo4cCup = 101,
        CuMclarenF1Cup = 102,
        DemoDerby = 106,
        Muscle2 = 108,
        Jdm2 = 109,
        HotHatch2 = 110,
        Coupe2 = 111,
        ClassicMuscle2 = 112,
        CuLanciaDeltaCup2 = 113,
        Modified2 = 114,
        Performance2 = 115,
        Gt2 = 116,
        CuNissanSkylineCup2 = 117,
        CuBmwE30M32 = 118,
        CuAlfaRomeo4cCup2 = 119,
        SuperModified2 = 120,
        Supercar2 = 121,
        Hypercar2 = 122,
        CuHondaNsxCup2 = 123,
        CuMclarenF1Cup2 = 124,
        CuCaterhamLolaCup2 = 125,
        CuLightweightCup2 = 126,
        CuArielAtomV8Cup2 = 127,
        CuSuperLightweightCup2 = 128,
        CuClassicMiniCup2 = 129,
        CuMiniCup2 = 130,
        ClassicTouringCarCup = 131,
        CuMazda787bCup2 = 133,
        CuFordGt40Cup2 = 135,
        CuShelbyCup2 = 136
    }

    public enum GridAutosportRaceType
    {
        Race = 2,
        TimeAttack = 4,
        Drift = 7,
        Eliminator = 14,
        Overtake = 17,
        Checkpoint = 22,
        Endurance = 23,
        Drag = 32,
        TimeTrial = 34,
        DemoDerby = 35,
        Sprint = 38
    }

    public enum GridAutosportLocation
    {
        Various = 11,
        Barcelona = 25,
        Chicago = 26,
        Dubai = 27,
        Paris = 29,
        Algarve = 30,
        BrandsHatch = 31,
        Indianapolis = 32,
        YasMarina = 39,
        RedBullRing = 40,
        SanFrancisco = 41,
        Washington = 42,
        MontTremblant = 47,
        Austin = 49,
        CallahanOval = 50,
        Hockenheim = 51,
        Jarama = 52,
        OkutamaGp = 53,
        Sepang = 54,
        IstanbulPark = 55,
        SpaFrancorchamps = 56,
        Bathurst = 57,
        Detroit = 59,
        California = 60,
        CoteDAzur = 61,
        HongKong = 62,
        Okutama = 63
    }

    public enum GridAutosportTrackModel
    {
        Skipfe = 118,
        Skipfe183 = 183,
        Speedway = 184,
        BarcelonaRoute0 = 318,
        BarcelonaRoute1 = 320,
        BarcelonaRoute2 = 321,
        BarcelonaRoute3 = 322,
        BarcelonaRoute4 = 323,
        BarcelonaRoute5 = 324,
        ChicagoRoute0 = 325,
        ChicagoRoute1 = 326,
        ChicagoRoute2 = 327,
        ChicagoRoute3 = 328,
        ChicagoRoute4 = 329,
        ChicagoRoute5 = 330,
        DubaiRoute0 = 333,
        DubaiRoute1 = 334,
        DubaiRoute2 = 335,
        DubaiRoute3 = 336,
        DubaiRoute4 = 337,
        DubaiRoute5 = 338,
        ParisRoute0 = 345,
        ParisRoute1 = 346,
        ParisRoute2 = 347,
        ParisRoute3 = 348,
        ParisRoute4 = 349,
        ParisRoute5 = 350,
        AlgarveRoute0 = 351,
        AlgarveRoute1 = 352,
        AlgarveRoute2 = 353,
        AlgarveRoute3 = 354,
        BrandsHatchRoute0 = 355,
        BrandsHatchRoute1 = 356,
        BrandsHatchRoute2 = 357,
        BrandsHatchRoute3 = 358,
        IndianapolisRoute0 = 359,
        IndianapolisRoute3 = 362,
        IndianapolisRoute4 = 363,
        IndianapolisRoute6 = 365,
        IndianapolisRoute7 = 366,
        RedBullRingRoute0 = 373,
        RedBullRingRoute1 = 374,
        RedBullRingRoute2 = 375,
        RedBullRingRoute3 = 376,
        RedBullRingRoute4 = 377,
        RedBullRingRoute5 = 378,
        YasMarinaRoute0 = 379,
        YasMarinaRoute1 = 380,
        YasMarinaRoute2 = 381,
        YasMarinaRoute3 = 382,
        YasMarinaRoute4 = 383,
        SanFranciscoRoute0 = 424,
        SanFranciscoRoute1 = 425,
        SanFranciscoRoute2 = 426,
        SanFranciscoRoute3 = 427,
        SanFranciscoRoute4 = 428,
        SanFranciscoRoute5 = 429,
        WashingtonRoute0 = 431,
        WashingtonRoute1 = 432,
        WashingtonRoute2 = 433,
        WashingtonRoute3 = 434,
        WashingtonRoute4 = 435,
        WashingtonRoute5 = 436,
        MontTremblantRoute0 = 459,
        MontTremblantRoute1 = 460,
        MontTremblantRoute2 = 461,
        MontTremblantRoute3 = 462,
        MontTremblantRoute4 = 463,
        MontTremblantRoute5 = 464,
        AustinRoute0 = 471,
        AustinRoute1 = 472,
        AustinRoute2 = 473,
        AustinRoute3 = 474,
        AustinRoute4 = 475,
        AustinRoute5 = 476,
        CallahanOvalRoute0 = 477,
        CallahanOvalRoute1 = 478,
        CallahanOvalRoute2 = 479,
        CallahanOvalRoute3 = 480,
        CallahanOvalRoute4 = 481,
        CallahanOvalRoute5 = 482,
        JaramaRoute0 = 483,
        JaramaRoute1 = 484,
        HockenheimRoute0 = 485,
        HockenheimRoute1 = 486,
        HockenheimRoute2 = 487,
        HockenheimRoute3 = 488,
        HockenheimRoute4 = 489,
        HockenheimRoute5 = 490,
        OkutamaGpRoute0 = 491,
        OkutamaGpRoute1 = 492,
        OkutamaGpRoute2 = 493,
        OkutamaGpRoute3 = 494,
        OkutamaGpRoute4 = 495,
        OkutamaGpRoute5 = 496,
        SepangRoute0 = 498,
        SepangRoute1 = 499,
        SepangRoute2 = 500,
        SepangRoute3 = 501,
        SepangRoute4 = 502,
        SepangRoute5 = 503,
        IstanbulParkRoute0 = 504,
        IstanbulParkRoute1 = 505,
        IstanbulParkRoute2 = 506,
        IstanbulParkRoute3 = 507,
        YasMarinaRoute5 = 508,
        SpaRoute0 = 512,
        SpaRoute1 = 513,
        SpaRoute2 = 514,
        SpaRoute3 = 515,
        BathurstRoute0 = 516,
        BathurstRoute1 = 517,
        BrandsHatchRoute4 = 522,
        BrandsHatchRoute5 = 523,
        DetroitRoute8 = 524,
        DetroitRoute9 = 525,
        CallahanOvalRoute7 = 526,
        YasMarinaRoute7 = 527,
        CaliforniaRoute0 = 532,
        CaliforniaRoute1 = 533,
        CaliforniaRoute2 = 534,
        CaliforniaRoute3 = 535,
        CaliforniaRoute4 = 536,
        CaliforniaRoute5 = 537,
        CoteDAzurRoute0 = 538,
        CoteDAzurRoute1 = 539,
        CoteDAzurRoute2 = 540,
        CoteDAzurRoute3 = 541,
        CoteDAzurRoute4 = 542,
        CoteDAzurRoute5 = 543,
        HongKongRoute0 = 544,
        HongKongRoute1 = 545,
        HongKongRoute2 = 546,
        HongKongRoute3 = 547,
        HongKongRoute4 = 548,
        HongKongRoute5 = 549,
        OkutamaP2pRoute0 = 550,
        OkutamaP2pRoute1 = 551,
        OkutamaP2pRoute2 = 552,
        OkutamaP2pRoute3 = 553,
        OkutamaP2pRoute4 = 554,
        OkutamaP2pRoute5 = 555
    }

    public enum GridAutosportTrackModelConditions
    {
        ChicagoRoute3Day = 296,
        ChicagoRoute5Day = 302,
        ChicagoRoute2Day = 303,
        ParisRoute5Day = 308,
        BarcelonaRoute2Day = 310,
        BarcelonaRoute4Day = 311,
        ParisRoute3Day = 317,
        BarcelonaRoute3Day = 319,
        BarcelonaRoute5Day = 322,
        ParisRoute4Day = 323,
        RedBullRingRoute4Day = 326,
        BrandsHatchRoute2Day = 327,
        ParisRoute2Day = 333,
        DubaiRoute4Day = 336,
        DubaiRoute3Day = 340,
        YasMarinaRoute4Day = 358,
        YasMarinaRoute3Day = 359,
        YasMarinaRoute2Day = 360,
        DubaiRoute2Day = 365,
        DubaiRoute0Day = 367,
        IndianapolisRoute6Day = 386,
        RedBullRingRoute0Day = 387,
        YasMarinaRoute1Day = 388,
        BarcelonaRoute1Day = 392,
        DubaiRoute5Day = 393,
        ParisRoute1Day = 397,
        ChicagoRoute1Day = 400,
        RedBullRingRoute1Day = 408,
        IndianapolisRoute3Day = 409,
        YasMarinaRoute0Day = 410,
        BarcelonaRoute0Day = 415,
        ParisRoute0Day = 420,
        DubaiRoute1Day = 422,
        ChicagoRoute0Day = 446,
        IndianapolisRoute7Day = 477,
        YasMarinaRoute0Night = 504,
        YasMarinaRoute1Night = 505,
        YasMarinaRoute2Night = 506,
        YasMarinaRoute3Night = 507,
        YasMarinaRoute4Night = 508,
        IndianapolisRoute0Day = 509,
        ChicagoRoute4Day = 519,
        IndianapolisRoute4Day = 550,
        RedBullRingRoute2Day = 551,
        RedBullRingRoute3Day = 552,
        RedBullRingRoute5Day = 553,
        BrandsHatchRoute0Day = 554,
        BrandsHatchRoute1Day = 555,
        BrandsHatchRoute3Day = 556,
        AlgarveRoute0Day = 557,
        AlgarveRoute1Day = 558,
        AlgarveRoute2Day = 559,
        AlgarveRoute3Day = 560,
        SanFranciscoRoute0Day = 572,
        YasMarinaRoute5Day = 573,
        SanFranciscoRoute1Day = 576,
        SanFranciscoRoute2Day = 577,
        SanFranciscoRoute3Day = 578,
        SanFranciscoRoute4Day = 579,
        SanFranciscoRoute5Day = 580,
        WashingtonRoute0Day = 589,
        WashingtonRoute1Day = 590,
        WashingtonRoute2Day = 591,
        WashingtonRoute3Day = 592,
        WashingtonRoute4Day = 593,
        WashingtonRoute5Day = 594,
        MontTremblantRoute0Day = 610,
        MontTremblantRoute1Day = 611,
        MontTremblantRoute2Day = 612,
        MontTremblantRoute3Day = 613,
        MontTremblantRoute4Day = 614,
        MontTremblantRoute5Day = 615,
        Austin0Day = 622,
        Austin1Day = 623,
        Austin2Day = 624,
        Austin3Day = 625,
        Austin4Day = 626,
        Austin5Day = 627,
        CallahanOval0Day = 628,
        CallahanOval1Day = 629,
        CallahanOval2Day = 630,
        CallahanOval3Day = 631,
        CallahanOval4Day = 632,
        CallahanOval5Day = 633,
        HockenheimRoute0Day = 635,
        HockenheimRoute1Day = 636,
        HockenheimRoute2Day = 637,
        HockenheimRoute3Day = 638,
        HockenheimRoute4Day = 639,
        HockenheimRoute5Day = 640,
        JaramaRoute0Day = 642,
        JaramaRoute1Day = 643,
        OkutamaGpRoute0Day = 644,
        OkutamaGpRoute1Day = 645,
        OkutamaGpRoute2Day = 646,
        OkutamaGpRoute3Day = 647,
        OkutamaGpRoute4Day = 648,
        OkutamaGpRoute5Day = 649,
        SepangRoute0Day = 650,
        SepangRoute1Day = 651,
        SepangRoute2Day = 652,
        SepangRoute3Day = 653,
        SepangRoute4Day = 654,
        SepangRoute5Day = 655,
        IstanbulParkRoute0Day = 656,
        IstanbulParkRoute1Day = 657,
        IstanbulParkRoute2Day = 658,
        IstanbulParkRoute3Day = 659,
        BrandsHatchRoute0Night = 661,
        Austin0Night = 662,
        SepangRoute0Night = 663,
        SpaRoute0Day = 664,
        SpaRoute1Day = 665,
        SpaRoute2Day = 666,
        SpaRoute3Day = 667,
        BathurstRoute0Day = 668,
        BathurstRoute1Day = 669,
        BrandsHatchRoute4Day = 672,
        BrandsHatchRoute5Day = 673,
        DetroitRoute8Day = 674,
        DetroitRoute9Day = 676,
        CallahanOval7Day = 677,
        YasMarinaRoute7Day = 678,
        Austin1Night = 679,
        Austin2Night = 680,
        Austin3Night = 681,
        Austin4Night = 682,
        Austin5Night = 683,
        SepangRoute1Night = 684,
        SepangRoute2Night = 685,
        SepangRoute3Night = 686,
        SepangRoute4Night = 687,
        SepangRoute5Night = 688,
        BrandsHatchRoute1Night = 689,
        BrandsHatchRoute2Night = 690,
        BrandsHatchRoute3Night = 691,
        SpaRoute0Night = 692,
        SpaRoute1Night = 693,
        SpaRoute2Night = 694,
        SpaRoute3Night = 695,
        AlgarveRoute0Night = 696,
        AlgarveRoute1Night = 697,
        AlgarveRoute2Night = 698,
        AlgarveRoute3Night = 699,
        MontTremblantRoute0Night = 700,
        MontTremblantRoute1Night = 701,
        MontTremblantRoute2Night = 702,
        MontTremblantRoute3Night = 703,
        MontTremblantRoute4Night = 704,
        MontTremblantRoute5Night = 705,
        HockenheimRoute0Night = 706,
        HockenheimRoute1Night = 707,
        HockenheimRoute2Night = 708,
        HockenheimRoute3Night = 709,
        OkutamaGpRoute0Night = 712,
        OkutamaGpRoute1Night = 713,
        OkutamaGpRoute3Night = 714,
        OkutamaGpRoute4Night = 715,
        CaliforniaRoute0Day = 719,
        CaliforniaRoute1Day = 720,
        CaliforniaRoute2Day = 721,
        CaliforniaRoute3Day = 722,
        CaliforniaRoute4Day = 723,
        CaliforniaRoute5Day = 724,
        CoteDAzurRoute0Day = 725,
        CoteDAzurRoute1Day = 726,
        CoteDAzurRoute2Day = 727,
        CoteDAzurRoute3Day = 728,
        CoteDAzurRoute4Day = 729,
        CoteDAzurRoute5Day = 730,
        HongKongRoute0Day = 731,
        HongKongRoute1Day = 732,
        HongKongRoute2Day = 733,
        HongKongRoute3Day = 734,
        HongKongRoute4Day = 735,
        HongKongRoute5Day = 736,
        OkutamaP2pRoute0Day = 737,
        OkutamaP2pRoute1Day = 738,
        OkutamaP2pRoute2Day = 739,
        OkutamaP2pRoute3Day = 740,
        OkutamaP2pRoute4Day = 741,
        OkutamaP2pRoute5Day = 742
    }

    public static readonly Dictionary<GridAutosportLocation, List<(GridAutosportTrackModel Track, List<GridAutosportTrackModelConditions> Conditions)>> LocationToTracksMap = new()
    {
        [GridAutosportLocation.Various] = new()
        {
            (GridAutosportTrackModel.Skipfe, newList<GridAutosportTrackModelConditions>()),
            (GridAutosportTrackModel.Skipfe183, newList<GridAutosportTrackModelConditions>()),
            (GridAutosportTrackModel.Speedway, newList<GridAutosportTrackModelConditions>())
        },
        [GridAutosportLocation.Barcelona] = new()
        {
            (GridAutosportTrackModel.BarcelonaRoute0, newList(GridAutosportTrackModelConditions.BarcelonaRoute0Day)),
            (GridAutosportTrackModel.BarcelonaRoute1, newList(GridAutosportTrackModelConditions.BarcelonaRoute1Day)),
            (GridAutosportTrackModel.BarcelonaRoute2, newList(GridAutosportTrackModelConditions.BarcelonaRoute2Day)),
            (GridAutosportTrackModel.BarcelonaRoute3, newList(GridAutosportTrackModelConditions.BarcelonaRoute3Day)),
            (GridAutosportTrackModel.BarcelonaRoute4, newList(GridAutosportTrackModelConditions.BarcelonaRoute4Day)),
            (GridAutosportTrackModel.BarcelonaRoute5, newList(GridAutosportTrackModelConditions.BarcelonaRoute5Day))
        },
        [GridAutosportLocation.Chicago] = new()
        {
            (GridAutosportTrackModel.ChicagoRoute0, newList(GridAutosportTrackModelConditions.ChicagoRoute0Day)),
            (GridAutosportTrackModel.ChicagoRoute1, newList(GridAutosportTrackModelConditions.ChicagoRoute1Day)),
            (GridAutosportTrackModel.ChicagoRoute2, newList(GridAutosportTrackModelConditions.ChicagoRoute2Day)),
            (GridAutosportTrackModel.ChicagoRoute3, newList(GridAutosportTrackModelConditions.ChicagoRoute3Day)),
            (GridAutosportTrackModel.ChicagoRoute4, newList(GridAutosportTrackModelConditions.ChicagoRoute4Day)),
            (GridAutosportTrackModel.ChicagoRoute5, newList(GridAutosportTrackModelConditions.ChicagoRoute5Day))
        },
        [GridAutosportLocation.Dubai] = new()
        {
            (GridAutosportTrackModel.DubaiRoute0, newList(GridAutosportTrackModelConditions.DubaiRoute0Day)),
            (GridAutosportTrackModel.DubaiRoute1, newList(GridAutosportTrackModelConditions.DubaiRoute1Day)),
            (GridAutosportTrackModel.DubaiRoute2, newList(GridAutosportTrackModelConditions.DubaiRoute2Day)),
            (GridAutosportTrackModel.DubaiRoute3, newList(GridAutosportTrackModelConditions.DubaiRoute3Day)),
            (GridAutosportTrackModel.DubaiRoute4, newList(GridAutosportTrackModelConditions.DubaiRoute4Day)),
            (GridAutosportTrackModel.DubaiRoute5, newList(GridAutosportTrackModelConditions.DubaiRoute5Day))
        },
        [GridAutosportLocation.Paris] = new()
        {
            (GridAutosportTrackModel.ParisRoute0, newList(GridAutosportTrackModelConditions.ParisRoute0Day)),
            (GridAutosportTrackModel.ParisRoute1, newList(GridAutosportTrackModelConditions.ParisRoute1Day)),
            (GridAutosportTrackModel.ParisRoute2, newList(GridAutosportTrackModelConditions.ParisRoute2Day)),
            (GridAutosportTrackModel.ParisRoute3, newList(GridAutosportTrackModelConditions.ParisRoute3Day)),
            (GridAutosportTrackModel.ParisRoute4, newList(GridAutosportTrackModelConditions.ParisRoute4Day)),
            (GridAutosportTrackModel.ParisRoute5, newList(GridAutosportTrackModelConditions.ParisRoute5Day))
        },
        [GridAutosportLocation.Algarve] = new()
        {
            (GridAutosportTrackModel.AlgarveRoute0, newList(GridAutosportTrackModelConditions.AlgarveRoute0Day, GridAutosportTrackModelConditions.AlgarveRoute0Night)),
            (GridAutosportTrackModel.AlgarveRoute1, newList(GridAutosportTrackModelConditions.AlgarveRoute1Day, GridAutosportTrackModelConditions.AlgarveRoute1Night)),
            (GridAutosportTrackModel.AlgarveRoute2, newList(GridAutosportTrackModelConditions.AlgarveRoute2Day, GridAutosportTrackModelConditions.AlgarveRoute2Night)),
            (GridAutosportTrackModel.AlgarveRoute3, newList(GridAutosportTrackModelConditions.AlgarveRoute3Day, GridAutosportTrackModelConditions.AlgarveRoute3Night))
        },
        [GridAutosportLocation.BrandsHatch] = new()
        {
            (GridAutosportTrackModel.BrandsHatchRoute0, newList(GridAutosportTrackModelConditions.BrandsHatchRoute0Day, GridAutosportTrackModelConditions.BrandsHatchRoute0Night)),
            (GridAutosportTrackModel.BrandsHatchRoute1, newList(GridAutosportTrackModelConditions.BrandsHatchRoute1Day, GridAutosportTrackModelConditions.BrandsHatchRoute1Night)),
            (GridAutosportTrackModel.BrandsHatchRoute2, newList(GridAutosportTrackModelConditions.BrandsHatchRoute2Day, GridAutosportTrackModelConditions.BrandsHatchRoute2Night)),
            (GridAutosportTrackModel.BrandsHatchRoute3, newList(GridAutosportTrackModelConditions.BrandsHatchRoute3Day, GridAutosportTrackModelConditions.BrandsHatchRoute3Night)),
            (GridAutosportTrackModel.BrandsHatchRoute4, newList(GridAutosportTrackModelConditions.BrandsHatchRoute4Day)),
            (GridAutosportTrackModel.BrandsHatchRoute5, newList(GridAutosportTrackModelConditions.BrandsHatchRoute5Day))
        },
        [GridAutosportLocation.Indianapolis] = new()
        {
            (GridAutosportTrackModel.IndianapolisRoute0, newList(GridAutosportTrackModelConditions.IndianapolisRoute0Day)),
            (GridAutosportTrackModel.IndianapolisRoute3, newList(GridAutosportTrackModelConditions.IndianapolisRoute3Day)),
            (GridAutosportTrackModel.IndianapolisRoute4, newList(GridAutosportTrackModelConditions.IndianapolisRoute4Day)),
            (GridAutosportTrackModel.IndianapolisRoute6, newList(GridAutosportTrackModelConditions.IndianapolisRoute6Day)),
            (GridAutosportTrackModel.IndianapolisRoute7, newList(GridAutosportTrackModelConditions.IndianapolisRoute7Day))
        },
        [GridAutosportLocation.YasMarina] = new()
        {
            (GridAutosportTrackModel.YasMarinaRoute0, newList(GridAutosportTrackModelConditions.YasMarinaRoute0Day, GridAutosportTrackModelConditions.YasMarinaRoute0Night)),
            (GridAutosportTrackModel.YasMarinaRoute1, newList(GridAutosportTrackModelConditions.YasMarinaRoute1Day, GridAutosportTrackModelConditions.YasMarinaRoute1Night)),
            (GridAutosportTrackModel.YasMarinaRoute2, newList(GridAutosportTrackModelConditions.YasMarinaRoute2Day, GridAutosportTrackModelConditions.YasMarinaRoute2Night)),
            (GridAutosportTrackModel.YasMarinaRoute3, newList(GridAutosportTrackModelConditions.YasMarinaRoute3Day, GridAutosportTrackModelConditions.YasMarinaRoute3Night)),
            (GridAutosportTrackModel.YasMarinaRoute4, newList(GridAutosportTrackModelConditions.YasMarinaRoute4Day, GridAutosportTrackModelConditions.YasMarinaRoute4Night)),
            (GridAutosportTrackModel.YasMarinaRoute5, newList(GridAutosportTrackModelConditions.YasMarinaRoute5Day)),
            (GridAutosportTrackModel.YasMarinaRoute7, newList(GridAutosportTrackModelConditions.YasMarinaRoute7Day))
        },
        [GridAutosportLocation.RedBullRing] = new()
        {
            (GridAutosportTrackModel.RedBullRingRoute0, newList(GridAutosportTrackModelConditions.RedBullRingRoute0Day)),
            (GridAutosportTrackModel.RedBullRingRoute1, newList(GridAutosportTrackModelConditions.RedBullRingRoute1Day)),
            (GridAutosportTrackModel.RedBullRingRoute2, newList(GridAutosportTrackModelConditions.RedBullRingRoute2Day)),
            (GridAutosportTrackModel.RedBullRingRoute3, newList(GridAutosportTrackModelConditions.RedBullRingRoute3Day)),
            (GridAutosportTrackModel.RedBullRingRoute4, newList(GridAutosportTrackModelConditions.RedBullRingRoute4Day)),
            (GridAutosportTrackModel.RedBullRingRoute5, newList(GridAutosportTrackModelConditions.RedBullRingRoute5Day))
        },
        [GridAutosportLocation.SanFrancisco] = new()
        {
            (GridAutosportTrackModel.SanFranciscoRoute0, newList(GridAutosportTrackModelConditions.SanFranciscoRoute0Day)),
            (GridAutosportTrackModel.SanFranciscoRoute1, newList(GridAutosportTrackModelConditions.SanFranciscoRoute1Day)),
            (GridAutosportTrackModel.SanFranciscoRoute2, newList(GridAutosportTrackModelConditions.SanFranciscoRoute2Day)),
            (GridAutosportTrackModel.SanFranciscoRoute3, newList(GridAutosportTrackModelConditions.SanFranciscoRoute3Day)),
            (GridAutosportTrackModel.SanFranciscoRoute4, newList(GridAutosportTrackModelConditions.SanFranciscoRoute4Day)),
            (GridAutosportTrackModel.SanFranciscoRoute5, newList(GridAutosportTrackModelConditions.SanFranciscoRoute5Day))
        },
        [GridAutosportLocation.Washington] = new()
        {
            (GridAutosportTrackModel.WashingtonRoute0, newList(GridAutosportTrackModelConditions.WashingtonRoute0Day)),
            (GridAutosportTrackModel.WashingtonRoute1, newList(GridAutosportTrackModelConditions.WashingtonRoute1Day)),
            (GridAutosportTrackModel.WashingtonRoute2, newList(GridAutosportTrackModelConditions.WashingtonRoute2Day)),
            (GridAutosportTrackModel.WashingtonRoute3, newList(GridAutosportTrackModelConditions.WashingtonRoute3Day)),
            (GridAutosportTrackModel.WashingtonRoute4, newList(GridAutosportTrackModelConditions.WashingtonRoute4Day)),
            (GridAutosportTrackModel.WashingtonRoute5, newList(GridAutosportTrackModelConditions.WashingtonRoute5Day))
        },
        [GridAutosportLocation.MontTremblant] = new()
        {
            (GridAutosportTrackModel.MontTremblantRoute0, newList(GridAutosportTrackModelConditions.MontTremblantRoute0Day, GridAutosportTrackModelConditions.MontTremblantRoute0Night)),
            (GridAutosportTrackModel.MontTremblantRoute1, newList(GridAutosportTrackModelConditions.MontTremblantRoute1Day, GridAutosportTrackModelConditions.MontTremblantRoute1Night)),
            (GridAutosportTrackModel.MontTremblantRoute2, newList(GridAutosportTrackModelConditions.MontTremblantRoute2Day, GridAutosportTrackModelConditions.MontTremblantRoute2Night)),
            (GridAutosportTrackModel.MontTremblantRoute3, newList(GridAutosportTrackModelConditions.MontTremblantRoute3Day, GridAutosportTrackModelConditions.MontTremblantRoute3Night)),
            (GridAutosportTrackModel.MontTremblantRoute4, newList(GridAutosportTrackModelConditions.MontTremblantRoute4Day, GridAutosportTrackModelConditions.MontTremblantRoute4Night)),
            (GridAutosportTrackModel.MontTremblantRoute5, newList(GridAutosportTrackModelConditions.MontTremblantRoute5Day, GridAutosportTrackModelConditions.MontTremblantRoute5Night))
        },
        [GridAutosportLocation.Austin] = new()
        {
            (GridAutosportTrackModel.AustinRoute0, newList(GridAutosportTrackModelConditions.Austin0Day, GridAutosportTrackModelConditions.Austin0Night)),
            (GridAutosportTrackModel.AustinRoute1, newList(GridAutosportTrackModelConditions.Austin1Day, GridAutosportTrackModelConditions.Austin1Night)),
            (GridAutosportTrackModel.AustinRoute2, newList(GridAutosportTrackModelConditions.Austin2Day, GridAutosportTrackModelConditions.Austin2Night)),
            (GridAutosportTrackModel.AustinRoute3, newList(GridAutosportTrackModelConditions.Austin3Day, GridAutosportTrackModelConditions.Austin3Night)),
            (GridAutosportTrackModel.AustinRoute4, newList(GridAutosportTrackModelConditions.Austin4Day, GridAutosportTrackModelConditions.Austin4Night)),
            (GridAutosportTrackModel.AustinRoute5, newList(GridAutosportTrackModelConditions.Austin5Day, GridAutosportTrackModelConditions.Austin5Night))
        },
        [GridAutosportLocation.CallahanOval] = new()
        {
            (GridAutosportTrackModel.CallahanOvalRoute0, newList(GridAutosportTrackModelConditions.CallahanOval0Day)),
            (GridAutosportTrackModel.CallahanOvalRoute1, newList(GridAutosportTrackModelConditions.CallahanOval1Day)),
            (GridAutosportTrackModel.CallahanOvalRoute2, newList(GridAutosportTrackModelConditions.CallahanOval2Day)),
            (GridAutosportTrackModel.CallahanOvalRoute3, newList(GridAutosportTrackModelConditions.CallahanOval3Day)),
            (GridAutosportTrackModel.CallahanOvalRoute4, newList(GridAutosportTrackModelConditions.CallahanOval4Day)),
            (GridAutosportTrackModel.CallahanOvalRoute5, newList(GridAutosportTrackModelConditions.CallahanOval5Day)),
            (GridAutosportTrackModel.CallahanOvalRoute7, newList(GridAutosportTrackModelConditions.CallahanOval7Day))
        },
        [GridAutosportLocation.Hockenheim] = new()
        {
            (GridAutosportTrackModel.HockenheimRoute0, newList(GridAutosportTrackModelConditions.HockenheimRoute0Day, GridAutosportTrackModelConditions.HockenheimRoute0Night)),
            (GridAutosportTrackModel.HockenheimRoute1, newList(GridAutosportTrackModelConditions.HockenheimRoute1Day, GridAutosportTrackModelConditions.HockenheimRoute1Night)),
            (GridAutosportTrackModel.HockenheimRoute2, newList(GridAutosportTrackModelConditions.HockenheimRoute2Day, GridAutosportTrackModelConditions.HockenheimRoute2Night)),
            (GridAutosportTrackModel.HockenheimRoute3, newList(GridAutosportTrackModelConditions.HockenheimRoute3Day, GridAutosportTrackModelConditions.HockenheimRoute3Night)),
            (GridAutosportTrackModel.HockenheimRoute4, newList(GridAutosportTrackModelConditions.HockenheimRoute4Day)),
            (GridAutosportTrackModel.HockenheimRoute5, newList(GridAutosportTrackModelConditions.HockenheimRoute5Day))
        },
        [GridAutosportLocation.Jarama] = new()
        {
            (GridAutosportTrackModel.JaramaRoute0, newList(GridAutosportTrackModelConditions.JaramaRoute0Day)),
            (GridAutosportTrackModel.JaramaRoute1, newList(GridAutosportTrackModelConditions.JaramaRoute1Day))
        },
        [GridAutosportLocation.OkutamaGp] = new()
        {
            (GridAutosportTrackModel.OkutamaGpRoute0, newList(GridAutosportTrackModelConditions.OkutamaGpRoute0Day, GridAutosportTrackModelConditions.OkutamaGpRoute0Night)),
            (GridAutosportTrackModel.OkutamaGpRoute1, newList(GridAutosportTrackModelConditions.OkutamaGpRoute1Day, GridAutosportTrackModelConditions.OkutamaGpRoute1Night)),
            (GridAutosportTrackModel.OkutamaGpRoute2, newList(GridAutosportTrackModelConditions.OkutamaGpRoute2Day)),
            (GridAutosportTrackModel.OkutamaGpRoute3, newList(GridAutosportTrackModelConditions.OkutamaGpRoute3Day, GridAutosportTrackModelConditions.OkutamaGpRoute3Night)),
            (GridAutosportTrackModel.OkutamaGpRoute4, newList(GridAutosportTrackModelConditions.OkutamaGpRoute4Day, GridAutosportTrackModelConditions.OkutamaGpRoute4Night)),
            (GridAutosportTrackModel.OkutamaGpRoute5, newList(GridAutosportTrackModelConditions.OkutamaGpRoute5Day))
        },
        [GridAutosportLocation.Sepang] = new()
        {
            (GridAutosportTrackModel.SepangRoute0, newList(GridAutosportTrackModelConditions.SepangRoute0Day, GridAutosportTrackModelConditions.SepangRoute0Night)),
            (GridAutosportTrackModel.SepangRoute1, newList(GridAutosportTrackModelConditions.SepangRoute1Day, GridAutosportTrackModelConditions.SepangRoute1Night)),
            (GridAutosportTrackModel.SepangRoute2, newList(GridAutosportTrackModelConditions.SepangRoute2Day, GridAutosportTrackModelConditions.SepangRoute2Night)),
            (GridAutosportTrackModel.SepangRoute3, newList(GridAutosportTrackModelConditions.SepangRoute3Day, GridAutosportTrackModelConditions.SepangRoute3Night)),
            (GridAutosportTrackModel.SepangRoute4, newList(GridAutosportTrackModelConditions.SepangRoute4Day, GridAutosportTrackModelConditions.SepangRoute4Night)),
            (GridAutosportTrackModel.SepangRoute5, newList(GridAutosportTrackModelConditions.SepangRoute5Day, GridAutosportTrackModelConditions.SepangRoute5Night))
        },
        [GridAutosportLocation.IstanbulPark] = new()
        {
            (GridAutosportTrackModel.IstanbulParkRoute0, newList(GridAutosportTrackModelConditions.IstanbulParkRoute0Day)),
            (GridAutosportTrackModel.IstanbulParkRoute1, newList(GridAutosportTrackModelConditions.IstanbulParkRoute1Day)),
            (GridAutosportTrackModel.IstanbulParkRoute2, newList(GridAutosportTrackModelConditions.IstanbulParkRoute2Day)),
            (GridAutosportTrackModel.IstanbulParkRoute3, newList(GridAutosportTrackModelConditions.IstanbulParkRoute3Day))
        },
        [GridAutosportLocation.SpaFrancorchamps] = new()
        {
            (GridAutosportTrackModel.SpaRoute0, newList(GridAutosportTrackModelConditions.SpaRoute0Day, GridAutosportTrackModelConditions.SpaRoute0Night)),
            (GridAutosportTrackModel.SpaRoute1, newList(GridAutosportTrackModelConditions.SpaRoute1Day, GridAutosportTrackModelConditions.SpaRoute1Night)),
            (GridAutosportTrackModel.SpaRoute2, newList(GridAutosportTrackModelConditions.SpaRoute2Day, GridAutosportTrackModelConditions.SpaRoute2Night)),
            (GridAutosportTrackModel.SpaRoute3, newList(GridAutosportTrackModelConditions.SpaRoute3Day, GridAutosportTrackModelConditions.SpaRoute3Night))
        },
        [GridAutosportLocation.Bathurst] = new()
        {
            (GridAutosportTrackModel.BathurstRoute0, newList(GridAutosportTrackModelConditions.BathurstRoute0Day)),
            (GridAutosportTrackModel.BathurstRoute1, newList(GridAutosportTrackModelConditions.BathurstRoute1Day))
        },
        [GridAutosportLocation.Detroit] = new()
        {
            (GridAutosportTrackModel.DetroitRoute8, newList(GridAutosportTrackModelConditions.DetroitRoute8Day)),
            (GridAutosportTrackModel.DetroitRoute9, newList(GridAutosportTrackModelConditions.DetroitRoute9Day))
        },
        [GridAutosportLocation.California] = new()
        {
            (GridAutosportTrackModel.CaliforniaRoute0, newList(GridAutosportTrackModelConditions.CaliforniaRoute0Day)),
            (GridAutosportTrackModel.CaliforniaRoute1, newList(GridAutosportTrackModelConditions.CaliforniaRoute1Day)),
            (GridAutosportTrackModel.CaliforniaRoute2, newList(GridAutosportTrackModelConditions.CaliforniaRoute2Day)),
            (GridAutosportTrackModel.CaliforniaRoute3, newList(GridAutosportTrackModelConditions.CaliforniaRoute3Day)),
            (GridAutosportTrackModel.CaliforniaRoute4, newList(GridAutosportTrackModelConditions.CaliforniaRoute4Day)),
            (GridAutosportTrackModel.CaliforniaRoute5, newList(GridAutosportTrackModelConditions.CaliforniaRoute5Day))
        },
        [GridAutosportLocation.CoteDAzur] = new()
        {
            (GridAutosportTrackModel.CoteDAzurRoute0, newList(GridAutosportTrackModelConditions.CoteDAzurRoute0Day)),
            (GridAutosportTrackModel.CoteDAzurRoute1, newList(GridAutosportTrackModelConditions.CoteDAzurRoute1Day)),
            (GridAutosportTrackModel.CoteDAzurRoute2, newList(GridAutosportTrackModelConditions.CoteDAzurRoute2Day)),
            (GridAutosportTrackModel.CoteDAzurRoute3, newList(GridAutosportTrackModelConditions.CoteDAzurRoute3Day)),
            (GridAutosportTrackModel.CoteDAzurRoute4, newList(GridAutosportTrackModelConditions.CoteDAzurRoute4Day)),
            (GridAutosportTrackModel.CoteDAzurRoute5, newList(GridAutosportTrackModelConditions.CoteDAzurRoute5Day))
        },
        [GridAutosportLocation.HongKong] = new()
        {
            (GridAutosportTrackModel.HongKongRoute0, newList(GridAutosportTrackModelConditions.HongKongRoute0Day)),
            (GridAutosportTrackModel.HongKongRoute1, newList(GridAutosportTrackModelConditions.HongKongRoute1Day)),
            (GridAutosportTrackModel.HongKongRoute2, newList(GridAutosportTrackModelConditions.HongKongRoute2Day)),
            (GridAutosportTrackModel.HongKongRoute3, newList(GridAutosportTrackModelConditions.HongKongRoute3Day)),
            (GridAutosportTrackModel.HongKongRoute4, newList(GridAutosportTrackModelConditions.HongKongRoute4Day)),
            (GridAutosportTrackModel.HongKongRoute5, newList(GridAutosportTrackModelConditions.HongKongRoute5Day))
        },
        [GridAutosportLocation.Okutama] = new()
        {
            (GridAutosportTrackModel.OkutamaP2pRoute0, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute0Day)),
            (GridAutosportTrackModel.OkutamaP2pRoute1, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute1Day)),
            (GridAutosportTrackModel.OkutamaP2pRoute2, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute2Day)),
            (GridAutosportTrackModel.OkutamaP2pRoute3, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute3Day)),
            (GridAutosportTrackModel.OkutamaP2pRoute4, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute4Day)),
            (GridAutosportTrackModel.OkutamaP2pRoute5, newList(GridAutosportTrackModelConditions.OkutamaP2pRoute5Day))
        }
    };

    private static List<T> newList<T>(params T[] items) => new List<T>(items);

    public static GridAutosportTrackModel GetTrackModel(this GridAutosportTrackModelConditions condition)
    {
        foreach (var loc in LocationToTracksMap.Values)
        {
            foreach (var (track, conditions) in loc)
            {
                if (conditions.Contains(condition))
                    return track;
            }
        }
        throw new KeyNotFoundException($"Nessun TrackModel associato alla condizione: {condition}");
    }

    public static GridAutosportLocation GetLocation(this GridAutosportTrackModel trackModel)
    {
        foreach (var (location, tracks) in LocationToTracksMap)
        {
            foreach (var (track, _) in tracks)
            {
                if (track == trackModel)
                    return location;
            }
        }
        throw new KeyNotFoundException($"Nessuna Location associata al TrackModel: {trackModel}");
    }

    public sealed record GridAutoSportChallengeRace(
        long RaceNetId,
        bool HigherIsBetter,
        GridAutosportDisciplineID DisciplineId,
        GridAutosportLocation LocationId,
        GridAutosportTrackModel TrackModelId,
        int TrackModelDlcId,
        GridAutosportTrackModelConditions ConditionsId,
        GridAutosportRaceType RaceTypeId,
        long RaceDuration,
        int VehicleTierId,
        GridAutosportVehicleClassID VehicleClassId,
        int VehicleId,
        int VehicleDlcId,
        bool SpecialRace,
        long GhostSlotId,
        int Rank,
        long PersonalBest,
        long PlatinumTarget,
        long GoldTarget,
        long SilverTarget,
        long BronzeTarget
    )
    {
        public GridAutoSportChallengeRace(
            long raceNetId,
            bool higherIsBetter,
            GridAutosportDisciplineID disciplineId,
            GridAutosportTrackModelConditions conditionsId,
            GridAutosportRaceType raceTypeId,
            long raceDuration,
            int vehicleTierId,
            GridAutosportVehicleClassID vehicleClassId,
            int vehicleId,
            int vehicleDlcId,
            bool specialRace,
            long ghostSlotId,
            int rank,
            long personalBest,
            long platinumTarget,
            long goldTarget,
            long silverTarget,
            long bronzeTarget
        ) : this(
            raceNetId,
            higherIsBetter,
            disciplineId,
            conditionsId.GetTrackModel().GetLocation(),
            conditionsId.GetTrackModel(),
            0,
            conditionsId,
            raceTypeId,
            raceDuration,
            vehicleTierId,
            vehicleClassId,
            vehicleId,
            vehicleDlcId,
            specialRace,
            ghostSlotId,
            rank,
            personalBest,
            platinumTarget,
            goldTarget,
            silverTarget,
            bronzeTarget
        )
        { }
    }
}
