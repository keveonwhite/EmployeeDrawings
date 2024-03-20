async function generateAndDownloadPdf(html, filename) {
    const doc = new jspdf.jsPDF({
        orientation: 'p',
        unit: 'pt',
        format: 'letter'
    });

    return new Promise((resolve, reject) => {
        doc.html(html, {
            callback: doc => {
                doc.save(filename);
                resolve();
            }
        });
    });
}
