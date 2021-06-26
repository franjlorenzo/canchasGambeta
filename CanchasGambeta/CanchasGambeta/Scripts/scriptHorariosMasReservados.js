google.charts.load('current', { packages: ['corechart', 'bar'] });
google.charts.setOnLoadCallback(drawBasic);

function drawBasic() {
    let titleArr = ['', 'Reservas'];
    let arrayNeeded = [];
    let arrHorarios = [];
    let arrCantidadReservas = [];

    $("table#tablaHorarios tr").each(function () {
        var arrayOfThisRow = [];
        var tableData = $(this).find('td');
        if (tableData.length > 0) {
            tableData.each(function () { arrayOfThisRow.push($(this).text()); });
            arrHorarios.push(arrayOfThisRow);
        }
    });

    let arrCanchasString = [];
    for (var i = 0; i < arrHorarios.length; i++) {
        let valor = arrHorarios[i];
        valor = $.trim(valor);
        arrCanchasString.push(valor);
    };

    $("table#tablaCantidadReservas tr").each(function () {
        var arrayOfThisRow = [];
        var tableData = $(this).find('td');
        if (tableData.length > 0) {
            tableData.each(function () { arrayOfThisRow.push($(this).text()); });
            arrCantidadReservas.push(arrayOfThisRow);
        }
    });

    let arrCantidadReservasString = [];
    for (var i = 0; i < arrCantidadReservas.length; i++) {
        let valor = arrCantidadReservas[i];
        valor = parseInt($.trim(valor));
        arrCantidadReservasString.push(valor);
    };

    arrayNeeded.push(titleArr);
    arrCanchasString.map(function (item, index) {
        let tempArr = [];
        tempArr.push(arrCanchasString[index], arrCantidadReservasString[index]);
        arrayNeeded.push(tempArr);
    });

    var data = google.visualization.arrayToDataTable(arrayNeeded);

    var options = {
        title: '',
        chartArea: { width: '50%' },
        backgroundColor: "#1A1A1D",

        hAxis: {
            title: 'Cantidad de reservas',
            titleTextStyle: { color: 'white' },
            minValue: 0,
            gridlineColor: 'white',
            textStyle: { color: 'white' }
        },
        vAxis: {
            title: 'Horarios',
            titleTextStyle: { color: 'white' },
            textStyle: { color: 'white' }
        },
        legend: '',
        legendTextStyle: {
            color: 'white'
        }
    };

    var chart = new google.visualization.BarChart(document.getElementById('chart_div'));

    chart.draw(data, options);
}