//Hitta patientens namn genom att slå in personnummer

document.getElementById("patientId").addEventListener("blur", async function () {
    let personnummer = this.value;
    if (personnummer) {
        try {

            //Hämtar information från API
            let response = await fetch(`https://localhost:7148/api/Patient/${personnummer}`); 

            // Läs in JSON-svaret EN gång
            const patient = await response.json();

            // Kontrollera om svaret är OK
            if (!response.ok) throw new Error("Patient ej hittad");

            // Sätt patientnamnet i formuläret
            document.getElementById("patientName").value = `${patient.fornamn} ${patient.efternamn}`;

        } catch (error) {
            alert("Kunde inte hämta patientinfo: " + error.message);
        }
    }
});

//Sparar information i JournalAPI

document.querySelector("form").addEventListener("submit", async function (event) {
    event.preventDefault();

    let journalData = {
        Personnummer: document.getElementById("patientId").value, // Personnummer från fältet
        Anteckning: document.getElementById("reasonForVisit").value  // Besöksorsak från fältet
    };

    try {
        let response = await fetch("https://localhost:7210/api/Journal", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(journalData)
        });

        if (!response.ok) throw new Error("Misslyckades att spara journal");
        alert("Journalanteckning sparad!");
    } catch (error) {
        alert("Fel vid sparande: " + error.message);
    }
});



