Payment Service: - Hanterar betalningar av skickade fakturor.

När en betalning är mottagen så skickas event.
När en faktura är sen så skickas event ?? invoice service lyssnar på det och skickar påminnelse/invoice på nytt?


för att hantera betalningar så lyssnar service på:
- invoice created så man vet betalperiod.
- invoice skickad så man vet att man ska börja räkna dagar om den är försenad,
- invoice id med ocr så man vet vilken betalningen ska matchas mot.



