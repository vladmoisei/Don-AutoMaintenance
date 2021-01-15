using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MVCWithBlazor.Data
{

    public enum Zona
    {
        [EnumMember(Value = "Ajustaj")]
        Ajustaj,
        [EnumMember(Value = "Laminor")]
        Laminor
    }

    public enum Status
    {
        [EnumMember(Value = "Nerezolvat")]
        Nerezolvat,
        [EnumMember(Value = "Rezolvat")]
        Rezolvat,
        [EnumMember(Value = "In Lucru")]
        InLucru
    }

    public enum Departament
    {
        [EnumMember(Value = "Mentenanta")]
        Mentenanta,
        [EnumMember(Value = "BirouTehnic")]
        BirouTehnic,
        [EnumMember(Value = "Ajustaj")]
        Ajustaj,
        [EnumMember(Value = "Laminor")]
        Laminor,
        [EnumMember(Value = "Calitate")]
        Calitate,
        [EnumMember(Value = "Achizitii")]
        Achizitii,
        [EnumMember(Value = "Opex")]
        Opex,
        [EnumMember(Value = "Mediu")]
        Mediu
    }

    public enum TipActiune
    {
        Zilnic,
        Saptamanal,
        Lunar,
        Trimestrial,
        Semestrial,
        Anual,
        Toate
    }

    public enum TipZi
    {
        W,
        Li,
        Co
    }
}
