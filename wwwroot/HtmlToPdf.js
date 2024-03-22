async function generateAndDownloadPdf(html, filename) {
    const doc = new jspdf.jsPDF({
        orientation: 'p',
        unit: 'pt',
        format: 'letter'
    });

    document.getElementsByClassName(".table").css("width:", "97%");
    document.getElementsByClassName(".table-row").css("width:", "95%");
    document.getElementsByClassName(".table-data").css("width:", "15%");

    return new Promise((resolve, reject) => {
        doc.html(html, {
            callback: doc => {
                doc.save(filename);
                resolve();
            }
        });
    });
}
