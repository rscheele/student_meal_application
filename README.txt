Het project kan met de .sln snelkoppeling in Visual Studio geopend worden. De versie die ik gebruikt heb is versie Microsoft Visual Studio Enterprise 2017 15.7.2

Om het project op te kunnen starten moeten eerst alle packages opnieuw geïmporteerd worden.

Daarna moet er in de package manager console 'update-database' als command uitgevoerd worden.

Het project runt nu in de debugger.

Known bugs:
De input validatie voor de prijzen gecodeerd voor Nederlandse formats (eg € 5,00 ipv $ 5.00 dus met een komma in plaats van punt). Als een browser niet in het Nederlands gelokaliseerd is kan de browser de gegevens niet correct in de database opslaan en onstaan er errors. Het is dus belangrijk dat de browser in het Nederlands staat! Een eventuele workaround is het commenten van de line
'[RegularExpression(@"^[0-9]{0,3}[,]{1}[0-9]{2,2}|[0-9]{1,3}$", ErrorMessage = "Geen geldige invoer voor prijs, gebruik de vorm '1,23' voor €1,12 of '5' voor €5,00 of '0' als je maaltijd gratis is.")]'
in Studentapplication -> Domain -> Entities -> Meal -> Line 39
Er is dan echter geen input validatie voor de prijs meer.

SUBMISSION:
 83eee140-3b6f-491c-803c-e572c766ce56