# Poslovna-WebServisi

--------------------------
##Uputstvo za bazu
- Izgenerisati bazu iz skripte
- U visual studiju otvoriti server explorer -> connect to database
- Server name podesiti na isti kao u management studiju
- Dole u drop down listi izabrati odgovarajucu bazu
- Test connection, Ok
- U web.config dole podesiti connection string, Data source promeniti iz VULETIC-PC\SQLEXPRESS u svoj server name(isti onaj iz management studija)


--------------------------
Back-end

Modeli u Models folder

Kontroleri u Controllers folder (Add -> Controller -> Web API 2 Controller with read/write actions)

--------------------------
Front-end

AngularJSFolder

JS modeli i kontroleri u podfoldere

--------------------------
Zahtevi

/api/{ime kontrolera}

/api/{ime kontrolera}/{id}
