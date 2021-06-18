namespace Signups.Core
open FSharp.Data
open System

// Start again from https://atlemann.github.io/fsharp/2018/02/28/fsharp-solutions-from-scratch.html

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

    type Parasha = 
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
        static member FromString(s: string) = TypeUtils.fromString<Parasha> s


    let getParashaFromTitle title =
        Parasha.FromString title


    type Year = A | B | C

    type ReadingRec = { Parasha: string; Reading: string }

    let CycleA = [
        {Parasha = "Bereshit"; Reading = "John 1:1-18"}
        {Parasha = "Noach"; Reading = "Matthew 1:1-17"}
        {Parasha = "Lech-Lecha"; Reading = "Matthew 1:18-25"}
        {Parasha = "Vayera"; Reading = "Matthew 2:1-12"}
        {Parasha = "Chayei Sara"; Reading = "Matthew 3:1-12"}
        {Parasha = "Toldot"; Reading = "Matthew 3:13-4:11"}
        {Parasha = "Vayetzei"; Reading = "Mark 1:14-28"}
        {Parasha = "Vayishlach"; Reading = "Mark 1:29-45"}
        {Parasha = "Vayeshev"; Reading = "Matthew 5:1-16"}
        {Parasha = "Miketz"; Reading = "Matthew 5:17-26"}
        {Parasha = "Vayigash"; Reading = "Matthew 5:27-48"}
        {Parasha = "Vayechi"; Reading = "Matthew 6:1-18"}
        {Parasha = "Shemot"; Reading = "Matthew 6:19-34"}
        {Parasha = "Vaera"; Reading = "Matthew 7:1-12"}
        {Parasha = "Bo"; Reading = "Matthew 7:13-29"}
        {Parasha = "Beshalach"; Reading = "Mark 2:1-12"}
        {Parasha = "Yitro"; Reading = "Matthew 11:2-19"}
        {Parasha = "Mishpatim"; Reading = "Matthew 11:20-30"}
        {Parasha = "Terumah"; Reading = "Matthew 13:1-23"}
        {Parasha = "Tetzaveh"; Reading = "Matthew 14:12-33"}
        {Parasha = "Ki Tisa"; Reading = "Matthew 15:1-20"}
        {Parasha = "Vayakhel"; Reading = "Matthew 15:21-28"}
        {Parasha = "Pekudei"; Reading = "Matthew 15:29-39"}
        {Parasha = "Vayakhel-Pekudei"; Reading = "Matthew 15:21-39"}
        {Parasha = ""; Reading = "Matthew "}
        {Parasha = ""; Reading = "Matthew "}
        {Parasha = ""; Reading = "Matthew "}
        {Parasha = ""; Reading = "Matthew "}
        {Parasha = ""; Reading = "Matthew "}
        {Parasha = ""; Reading = "Matthew "}
        ]

    let CycleB = [
        {Parasha = "Bereshit"; Reading = "John 1:1-18"}
        {Parasha = "Noach"; Reading = "Luke 1:26-38"}
        {Parasha = "Lech-Lecha"; Reading = "Luke 2:1-20"}
        {Parasha = "Vayera"; Reading = "Luke 2:21-40"}
        {Parasha = "Chayei Sara"; Reading = "Luke 3:1-17"}
        {Parasha = "Toldot"; Reading = "Luke 3:18-38"}
        {Parasha = "Vayetzei"; Reading = "Luke 4:1-15"}
        {Parasha = "Vayishlach"; Reading = "Luke 4:16-30"}
        {Parasha = "Vayeshev"; Reading = "Luke 4:31-44"}
        {Parasha = "Miketz"; Reading = "Luke 5:1-11"}
        {Parasha = "Vayigash"; Reading = "Luke 5:12-26"}
        {Parasha = "Vayechi"; Reading = "Luke 5:27-39"}
        {Parasha = "Shemot"; Reading = "Luke 6:1-16"}
        {Parasha = "Vaera"; Reading = "Luke 6:17-38"}
        {Parasha = "Bo"; Reading = "Luke 7:1-17"}
        {Parasha = "Beshalach"; Reading = "Luke 7:18-35"}
        {Parasha = "Yitro"; Reading = "Luke 7:36-50"}
        {Parasha = "Mishpatim"; Reading = "Luke 8:1-21"}
        {Parasha = "Terumah"; Reading = "Luke 8:22-39"}
        {Parasha = "Tetzaveh"; Reading = "Luke 8:40-56"}
        {Parasha = "Ki Tisa"; Reading = "Luke 9:1-17"}
        {Parasha = "Vayakhel"; Reading = "Luke 9:18-27"}
        {Parasha = "Pekudei"; Reading = "Luke 9:28-36"}
        {Parasha = "Vayakhel-Pekudei"; Reading = "Luke 9:18-36"}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        {Parasha = ""; Reading = "Luke "}
        ]

    let CycleC = [
        {Parasha = "Bereshit"; Reading = "John 1:1-18"}
        {Parasha = "Noach"; Reading = "John 1:19-34"}
        {Parasha = "Lech-Lecha"; Reading = "John 1:35-51"}
        {Parasha = "Vayera"; Reading = "John 2:1-12"}
        {Parasha = "Chayei Sara"; Reading = "John 2:13-25"}
        {Parasha = "Toldot"; Reading = "John 3:1-21"}
        {Parasha = "Vayetzei"; Reading = "John 4:5-30"}
        {Parasha = "Vayishlach"; Reading = "John 4:31-42"}
        {Parasha = "Vayeshev"; Reading = "John 4:43-54"}
        {Parasha = "Miketz"; Reading = "John 5:1-15"}
        {Parasha = "Vayigash"; Reading = "John 5:16-29"}
        {Parasha = "Vayechi"; Reading = "John 5:30-47"}
        {Parasha = "Shemot"; Reading = "John 6:1-15"}
        {Parasha = "Vaera"; Reading = "John 6:16-29"}
        {Parasha = "Bo"; Reading = "John 6:30-51"}
        {Parasha = "Beshalach"; Reading = "John 6:52-71"}
        {Parasha = "Yitro"; Reading = "John 7:1-13"}
        {Parasha = "Mishpatim"; Reading = "John 7:14-24"}
        {Parasha = "Terumah"; Reading = "John 7:25-36"}
        {Parasha = "Tetzaveh"; Reading = "John 7:37-52"}
        {Parasha = "Ki Tisa"; Reading = "John 8:1-11"}
        {Parasha = "Vayakhel"; Reading = "John 8:12-20"}
        {Parasha = "Pekudei"; Reading = "John 8:21-30"}
        {Parasha = "Vayakhel-Pekudei"; Reading = "John 8:12-30"}
        {Parasha = "Vayikra"; Reading = ""}
        {Parasha = "Tzav"; Reading = ""}
        {Parasha = "Shmini"; Reading = ""}
        {Parasha = "Tazria-Metzora"; Reading = ""}
        {Parasha = "Tazria"; Reading = ""}
        {Parasha = "Metzora"; Reading = ""}
        {Parasha = "Achrei Mot-Kedoshim"; Reading = ""}
        {Parasha = "Kedoshim"; Reading = ""}
        {Parasha = "Achrei Mot"; Reading = ""}
        {Parasha = "Emor"; Reading = ""}
        {Parasha = "Behar"; Reading = ""}
        {Parasha = "Bechukotai"; Reading = ""}
        {Parasha = "Behar-Bechukotai"; Reading = ""}
        {Parasha = "Bamidbar"; Reading = ""}
        {Parasha = "Nasso"; Reading = ""}
        {Parasha = "Beha'alotcha"; Reading = ""}
        {Parasha = "Sh'lach"; Reading = ""}
        {Parasha = "Korach"; Reading = ""}
        {Parasha = "Chukat"; Reading = ""}
        {Parasha = "Balak"; Reading = ""}
        {Parasha = "Pinchas"; Reading = ""}
        {Parasha = "Matot"; Reading = ""}
        {Parasha = "Masei"; Reading = ""}
        {Parasha = "Matot-Masei"; Reading = ""}
        {Parasha = "Devarim"; Reading = ""}
        {Parasha = "Vaetchanan"; Reading = ""}
        {Parasha = "Eikev"; Reading = ""}
        {Parasha = "Re'eh"; Reading = ""}
        {Parasha = "Shoftim"; Reading = ""}
        {Parasha = "Ki Teitzei"; Reading = ""}
        {Parasha = "Ki Tavo"; Reading = ""}
        {Parasha = "Nitzavim"; Reading = ""}
        {Parasha = "Vayeilech"; Reading = ""}
        {Parasha = "Ha'Azinu"; Reading = ""}
        ]

    let HolidayReadings = [
        {Parasha = "Sukkot"; Reading = ""}
        {Parasha = "Yom Kippur"; Reading = ""}
        {Parasha = "Rosh Hashana"; Reading = ""}
        {Parasha = "Shabbat Shuva"; Reading = ""}
        {Parasha = "Rosh Hashana II"; Reading = ""}
        {Parasha = "Shavuot I"; Reading = ""}
        ]

    let BesorotData (year: Year) =
        match year with
        | A -> CycleA
        | B -> CycleB
        | C -> CycleC
        
    type SpecialShabbat = Chanukah | Shekalim | Zachor | Parah | HaChodesh | HaGadol | Shuva

    let GetSpecialReadings special year = 
        match (special, year) with
        | (None,_) -> None
        | (Some SpecialShabbat.Chanukah), _ -> Some "John 10:22-42"
        | (Some SpecialShabbat.Shekalim), _ -> Some "Mark 12:41-44"
        | (_,_) -> None

    type WeeklyParasha = { Sedra: string; Shabbat: SpecialShabbat option}

    //type Readings =
    //    | year of Year
    //    | reading of list Reading

    let GetYear (calendarYear: int) = 
        match (calendarYear - 5771) % 3 with
        | 0 -> Year.A
        | 1 -> Year.B
        | _ -> Year.C

    let GetReading calendarYear parasha =
        let cycleYear = GetYear calendarYear
        let readings = BesorotData cycleYear

        let specialReading = GetSpecialReadings parasha.Shabbat cycleYear

        let reading = 
            match specialReading with
            | Some r -> Some { Parasha = parasha.Sedra; Reading = r }
            | None -> (readings |> Seq.tryFind(fun i -> i.Parasha = parasha.Sedra))

        reading

    let GetReadingBySedra calendarYear parasha specialShabbat =
        GetReading calendarYear { Sedra = parasha; Shabbat = specialShabbat }

        


