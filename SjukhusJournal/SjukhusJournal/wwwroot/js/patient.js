document.addEventListener("DOMContentLoaded", async function () {
    try {
        let response = await fetch("https://informatik12.ei.hv.se/PatientAPI/api/Patient"); // Hämta patientlistan från API:et
        if (!response.ok) throw new Error("Kunde inte hämta patientlistan.");

        let patients = await response.json();

        // Hitta div/tabellen där patientlistan ska visas
        let patientContainer = document.getElementById("patientList");
        patientContainer.innerHTML = ""; // Rensa tidigare innehåll

        if (patients.length === 0) {
            patientContainer.innerHTML = "<p class='text-center'>Inga patienter hittades.</p>";
            return;
        }

        // Skapa en tabell för att visa patienterna
        let table = document.createElement("table");
        table.className = "table table-striped"; // Bootstrap-klasser

        // Lägg till tabellhuvud
        table.innerHTML = `
            <thead>
                <tr>
                    <th>Förnamn</th>
                    <th>Efternamn</th>
                    <th>Personnummer</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        `;

        let tbody = table.querySelector("tbody");

        // Loopa genom patienterna och lägg till dem i tabellen
        patients.forEach(patient => {
            let row = document.createElement("tr");
            row.innerHTML = `
                <td>${patient.fornamn}</td>
                <td>${patient.efternamn}</td>
                <td>${patient.personnummer}</td>
            `;
            tbody.appendChild(row);
        });

        patientContainer.appendChild(table);
    } catch (error) {
        alert("Fel vid hämtning: " + error.message);
    }
});
