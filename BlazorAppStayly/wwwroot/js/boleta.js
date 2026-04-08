window.exportBoletaPdf = (boleta) => {
    const ticket = `
        <style>
        * { box-sizing: border-box; margin: 0; padding: 0; }
        .ticket {
            width: 280px;
            font-family: 'Courier New', monospace;
            font-size: 12px;
            padding: 14px 12px;
            color: #000;
        }
        .header {
            text-align: center;
            margin-bottom: 8px;
        }
        .header h4 {
            font-size: 14px;
            font-weight: bold;
            letter-spacing: 1px;
            margin-bottom: 2px;
        }
        .badge {
            display: inline-block;
            border: 1px solid #000;
            font-size: 10px;
            padding: 1px 8px;
            border-radius: 20px;
            margin-top: 4px;
        }
        .line {
            border: none;
            border-top: 1px dashed #000;
            margin: 8px 0;
        }
        .row {
            display: flex;
            justify-content: space-between;
            margin: 4px 0;
        }
        .row span:first-child {
            color: #555;
        }
        .row span:last-child {
            font-weight: 500;
            text-align: right;
            max-width: 60%;
        }
        .dates-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 6px;
            margin: 6px 0;
        }
        .date-box {
            background: #f5f5f5;
            border-radius: 4px;
            padding: 5px 7px;
        }
        .date-box .label {
            font-size: 10px;
            color: #777;
        }
        .date-box .value {
            font-size: 11px;
            font-weight: 600;
        }
        .date-box.full {
            grid-column: 1 / -1;
        }
        .descripcion {
            text-align: center;
            font-style: italic;
            font-size: 11px;
            color: #444;
            margin: 6px 0;
        }
        .total-box {
            background: #000;
            color: #fff;
            border-radius: 6px;
            padding: 8px 12px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 8px 0;
        }
        .total-box .label { font-size: 11px; }
        .total-box .amount { font-size: 16px; font-weight: bold; }
        .footer {
            text-align: center;
            font-size: 10px;
            color: #666;
            margin-top: 8px;
        }
        </style>

        <div class="ticket">
            <div class="header">
                <h4>BOLETA DE ALQUILER</h4>
            </div>

            <div class="line"></div>

            <div class="row">
                <span>Código</span>
                <span>${boleta.codigo}</span>
            </div>

            <div class="line"></div>

            <div class="dates-grid">
                <div class="date-box">
                    <div class="label">Fecha inicio</div>
                    <div class="value">${boleta.fecha}</div>
                </div>
                <div class="date-box">
                    <div class="label">Fecha fin</div>
                    <div class="value">${boleta.fechaFin}</div>
                </div>
                <div class="date-box full">
                    <div class="label">Fecha de pago</div>
                    <div class="value">${boleta.fechaPagoRealizado}</div>
                </div>
            </div>

            <div class="line"></div>

            <div class="row">
                <span>Cliente</span>
                <span>${boleta.nombreCliente}</span>
            </div>
            <div class="row">
                <span>N° Documento</span>
                <span>${boleta.numDocumento}</span>
            </div>

            <div class="line"></div>

            <div class="descripcion">${boleta.descripcion}</div>

            <div class="line"></div>

            <div class="total-box">
                <span class="label">TOTAL</span>
                <span class="amount">S/ ${boleta.total}</span>
            </div>

            <div class="footer">Gracias por su preferencia</div>
        </div>
    `;

    const element = document.createElement("div");
    element.innerHTML = ticket;
    document.body.appendChild(element);

    html2pdf()
        .from(element)
        .set({
            margin: 0,
            filename: `boleta_${boleta.codigo}.pdf`,
            html2canvas: { scale: 3, useCORS: true },
            jsPDF: { unit: 'mm', format: [80, 160], orientation: 'portrait' }
        })
        .save()
        .then(() => document.body.removeChild(element));
};