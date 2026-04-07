window.exportBoletaPdf = (boleta) => {

    const ticket = `
        <style>

        .ticket{
            width:280px;
            font-family:monospace;
            font-size:13px;
            padding:10px;
            color:black;
        }

        .center{
            text-align:center;
        }

        .line{
            border-top:1px dashed black;
            margin:6px 0;
        }

        .row{
            display:flex;
            justify-content:space-between;
        }

        .total{
            text-align:center;
            font-weight:bold;
            font-size:15px;
        }

        </style>

        <div class="ticket">

            <div class="center">
                <h4>BOLETA DE ALQUILER</h4>
            </div>

            <div class="line"></div>

            <div class="row">
                <span>Código:</span>
                <span>${boleta.codigo}</span>
            </div>

            <div class="row">
                <span>Fecha Incio:</span>
                <span>${boleta.fecha}</span>
                <span>Fecha Fin:</span>
                <span>${boleta.fechaFin}</span>
            </div>

            <div class="row">
                <span>Fecha de Pago:</span>
                <span>${boleta.fechaPagoRealizado}</span>
            </div>

            <div class="line"></div>

            <div class="row">
                <span>Cliente:</span>
                <span>${boleta.nombreCliente}</span>
            </div>

            <div class="row">
                <span>Doc:</span>
                <span>${boleta.numDocumento}</span>
            </div>

            <div class="line"></div>

            <div class="center">
                ${boleta.descripcion}
            </div>

            <div class="line"></div>

            <div class="total">
                TOTAL: S/ ${boleta.total}
            </div>

            <div class="line"></div>

            <div class="center">
                Gracias por su preferencia
            </div>

        </div>
    `;

    const element = document.createElement("div");
    element.innerHTML = ticket;

    html2pdf()
        .from(element)
        .set({
            margin: 0,
            filename: `boleta_${boleta.codigo}.pdf`,
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'mm', format: [80, 120], orientation: 'portrait' }
        })
        .save();
}