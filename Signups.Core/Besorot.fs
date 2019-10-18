namespace Signups.Core
open FSharp.Data
open System

module TypeUtils = 
    open Microsoft.FSharp.Reflection

    let toString (x:'a) = 
        match FSharpValue.GetUnionFields(x, typeof<'a>) with
        | case, _ -> case.Name

    let fromString<'a> (s:string) =
        match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
        |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
        |_ -> None

module Besorot =
    type BesorahReadings = JsonProvider<"./besorah-data.json">
    //type Reading = {
    //    parasha: string;
    //    reading: string
    //}

    type ShabbatType = 
        ``Bereshit``
        |``Noach``
        |``Lech-Lecha``
        |``Vayera``
        |``Chayei Sara``
        |``Toldot``
        |``Vayetzei``
        |``Vayishlach``
        |``Vayeshev``
        |``Miketz``
        |``Vayigash``
        |``Vayechi``
        |``Shemot``
        |``Vaera``
        |``Bo``
        |``Beshalach``
        |``Yitro``
        |``Mishpatim``
        |``Shabbat Shekalim``
        |``Terumah``
        |``Tetzaveh``
        |``Shabbat Zachor``
        |``Erev Purim``
        |``Purim``
        |``Ki Tisa``
        |``Vayakhel-Pekudei``
        |``Shabbat Parah``
        |``Vayikra``
        |``Shabbat HaChodesh``
        |``Tzav``
        |``Shabbat HaGadol``
        |``Pesach I``
        |``Pesach VII``
        |``Pesach VIII``
        |``Shmini``
        |``Tazria-Metzora``
        |``Achrei Mot-Kedoshim``
        |``Emor``
        |``Behar-Bechukotai``
        |``Bamidbar``
        |``Shavuot I``
        |``Shavuot II``
        |``Nasso``
        |``Beha'alotcha``
        |``Sh'lach``
        |``Korach``
        |``Chukat``
        |``Balak``
        |``Pinchas``
        |``Rosh Chodesh Av``
        |``Matot-Masei``
        |``Devarim``
        |``Shabbat Chazon``
        |``Vaetchanan``
        |``Shabbat Nachamu``
        |``Eikev``
        |``Re'eh``
        |``Shoftim``
        |``Ki Teitzei``
        |``Ki Tavo``
        |``Nitzavim``
        |``Rosh Hashana I``
        |``Rosh Hashana II``
        |``Vayeilech``
        |``Shabbat Shuva``
        |``Erev Yom Kippur``
        |``Yom Kippur``
        |``Ha'Azinu``
        |``Sukkot I``
        |``Sukkot II``
        |``Sukkot VII (Hoshana Raba)``
        |``Shmini Atzeret``
        |``Simchat Torah``
        override this.ToString() = TypeUtils.toString this
        static member FromString(s: string) = TypeUtils.fromString<ShabbatType> s


    let getParashaFromTitle title =
        ShabbatType.FromString title


    type Year = A | B | C

    type ReadingRec = { Parasha: string; Reading: string }

    let CycleA = [
        {Parasha = "Bereshit"; Reading = "John 1:1–18"}
        ]

    let CycleB = [
        {Parasha = "Bereshit"; Reading = "John 1:1–18"}
        ]

    let CycleC = [
        {Parasha = "Bereshit"; Reading = "John 1:1–18"}
        {Parasha = "Noach"; Reading = "John 1:19-34"}
        {Parasha = "Lech-Lecha"; Reading = "John 1:35-51"}
        {Parasha = "Vayera"; Reading = "John 2:1–12"}
        {Parasha = "Chayei Sara"; Reading = "John 2:13–25"}
        {Parasha = "Toldot"; Reading = "John 3:1–21"}
        {Parasha = "Vayetzei"; Reading = "John 4:5–30"}
        {Parasha = "Vayishlach"; Reading = "John 4:31–42"}
        {Parasha = "Vayeshev"; Reading = "John 4:43–54"}
        {Parasha = "Miketz"; Reading = "John 5:1–15"}
        {Parasha = "Vayigash"; Reading = "John 5:16–29"}
        {Parasha = "Vayechi"; Reading = "John 5:30–47"}
        {Parasha = "Shemot"; Reading = "John 6:1–15"}
        {Parasha = "Vaera"; Reading = "John 6:16–29"}
        {Parasha = "Bo"; Reading = "John 6:30–51"}
        {Parasha = "Beshalach"; Reading = "John 6:52–71"}
        {Parasha = "Yitro"; Reading = "John 7:1–13"}
        {Parasha = "Mishpatim"; Reading = "John 7:14–24"}
        {Parasha = "Terumah"; Reading = "John 7:25–36"}
        {Parasha = "Tetzaveh"; Reading = "John 7:37–52"}
        {Parasha = "Ki Tisa"; Reading = "John 8:1–11"}
        {Parasha = "Vayakhel"; Reading = "John 8:12–20"}
        {Parasha = "Pekudei"; Reading = "John 8:21–30"}
        {Parasha = "Vayakhel-Pekudei"; Reading = "John 8:12–30"}
        ]

    let BesorotData (year: Year) =
        match year with
        | A -> CycleA
        | B -> CycleB
        | C -> CycleC
        
    type SpecialShabbat = Chanukah | Shekalim | Zachor | Parah | HaChodesh | HaGadol | Shuva

    type Parasha = { Sedra: string; Shabbat: SpecialShabbat option}

    //type Readings =
    //    | year of Year
    //    | reading of list Reading

    let GetYear (calendarYear: int) = 
        match (calendarYear - 5771) % 3 with
        | 0 -> Year.A
        | 1 -> Year.B
        | _ -> Year.C

    let GetReading calendarYear parasha =
        let year = GetYear calendarYear
        let readings = BesorotData year

        let found = readings |> Seq.find(fun i -> i.Parasha = parasha.Sedra)
        found

    let GetReadingBySedra calendarYear parasha =
        GetReading calendarYear { Sedra = parasha; Shabbat = None }

        


