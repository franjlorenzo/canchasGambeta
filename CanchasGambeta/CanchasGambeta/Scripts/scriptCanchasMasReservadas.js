google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    let titleArr = ['Canchas', 'Cantidad de reservas'];
    let arrayNeeded = [];
    let arrCanchas = [];
    let arrCantidadReservas = [];

    $("table#tablaCanchas tr").each(function () {
        var arrayOfThisRow = [];
        var tableData = $(this).find('td');
        if (tableData.length > 0) {
            tableData.each(function () { arrayOfThisRow.push($(this).text()); });
            arrCanchas.push(arrayOfThisRow);
        }
    });

    let arrCanchasString = [];
    for (var i = 0; i < arrCanchas.length; i++) {
        let valor = arrCanchas[i];
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
        titleTextStyle: {
            color: 'white',
            fontSize: 30
        },
        backgroundColor: '#1A1A1D',
        legend: '',
        legendTextStyle: {
            color: 'white'
        }
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
}