async function generateAndDownloadPdf(html, filename) {
    const doc = new jspdf.jsPDF({
        orientation: 'p',
        unit: 'in',
        format: [8.5, 11]
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
