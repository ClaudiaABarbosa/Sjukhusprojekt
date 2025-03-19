//Hämta Journalanteckningar
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
            let journalCard = document.createElement("div");
            journalCard.className = "card mb-3";
            journalCard.innerHTML = `
                    <div class="card-body">
                        <p class="card-text"><strong>Besöksorsak:</strong> ${journal.anteckning}</p>
                    </div>
                `;
            journalContainer.appendChild(journalCard);
        });
    } catch (error) {
        alert("Fel vid hämtning: " + error.message);
    }
});