// Hämta journalanteckningar
document.getElementById("searchJournal").addEventListener("click", async function () {
    let personnummer = document.getElementById("patientIdSearch").value;
    if (!personnummer) {
        alert("Vänligen ange ett personnummer.");
        return;
    }

    try {
        let response = await fetch(`https://informatik12.ei.hv.se/JournalAPI/api/Journal/${personnummer}`);
        if (!response.ok) throw new Error("Kunde inte hämta journaler.");
        let journals = await response.json();

        let journalContainer = document.getElementById("journalEntries");
        journalContainer.innerHTML = ""; // Rensa gamla journaler

        if (journals.length === 0) {
            journalContainer.innerHTML = "<p class='text-center'>Inga journalanteckningar hittades.</p>";
            return;
        }

        // Sortera journalerna från nyast till äldst
        journals.sort((a, b) => b.journalId - a.journalId);

        journals.forEach(journal => {
            console.log("Journal-ID: ", journal.journalId); // Kontrollera att ID finns

            let journalCard = document.createElement("div");
            journalCard.className = "card mb-3";
            journalCard.innerHTML = `
                <div class="card-body">
                    <p class="card-text"><strong>Besöksorsak:</strong> ${journal.anteckning}</p>
                    <button class="btn btn-warning edit-btn mt-2" 
                        data-journalid="${journal.journalId}" 
                        data-personnummer="${personnummer}">Redigera</button>
                </div>
            `;
            journalContainer.appendChild(journalCard);
        });

    } catch (error) {
        alert("Fel vid hämtning: " + error.message);
    }
});

// Lägg event-listener på journalEntries (för att fånga dynamiska knappar)
document.getElementById("journalEntries").addEventListener("click", function (event) {
    if (event.target.classList.contains("edit-btn")) {
        console.log("Redigera-knapp klickad!"); // Kontrollera om det funkar

        let journalId = event.target.dataset.journalid;
        let personnummer = event.target.dataset.personnummer;
        let currentText = event.target.closest(".card-body").querySelector(".card-text").textContent.replace("Besöksorsak: ", "");

        document.getElementById("editJournalText").value = currentText;
        document.getElementById("saveEditJournal").dataset.journalid = journalId;
        document.getElementById("saveEditJournal").dataset.personnummer = personnummer; // Spara personnummer

        console.log("JournalID sparat på knappen:", journalId);
        console.log("Personnummer sparat på knappen:", personnummer);

        let editModal = new bootstrap.Modal(document.getElementById("editJournalModal"));
        editModal.show();
    }
});

// Spara uppdaterad journalanteckning
document.getElementById("saveEditJournal").addEventListener("click", async function () {
    let journalId = this.dataset.journalid;
    let personnummer = this.dataset.personnummer; // Hämta personnumret
    let updatedText = document.getElementById("editJournalText").value;

    console.log("Spara-knappen klickad! JournalID:", journalId, "Personnummer:", personnummer); // Kontrollera ID

    if (!updatedText) {
        alert("Journalanteckningen får inte vara tom.");
        return;
    }

    try {
        let response = await fetch(`https://informatik12.ei.hv.se/JournalAPI/api/Journal/${journalId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                anteckning: updatedText,
                personnummer: personnummer // Skicka personnumret 
            })
        });

        if (!response.ok) throw new Error("Kunde inte uppdatera journalanteckningen.");

        alert("Journalanteckningen har uppdaterats!");

        // Uppdatera anteckningen i listan utan att ladda om sidan
        let updatedCard = document.querySelector(`[data-journalid="${journalId}"]`).closest(".card-body");
        updatedCard.querySelector(".card-text").innerHTML = `<strong>Besöksorsak:</strong> ${updatedText}`;

        // Stäng modalen
        let editModal = bootstrap.Modal.getInstance(document.getElementById("editJournalModal"));
        editModal.hide();
    } catch (error) {
        alert("Fel vid uppdatering: " + error.message);
    }
});
