﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Data
{

    public enum Zona
    {
        Ajustaj,
        Laminor
    }

    public enum Status
    {
        Nerezolvat,
        Rezolvat,
        InLucru
    }

    public enum Departament
    {
        Mentenanta,
        BirouTehnic,
        Ajustaj,
        Laminor,
        Calitate,
        Achizitii,
        Opex,
        Mediu
    }
}
