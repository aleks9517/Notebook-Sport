function createChartParams(labels, values) {
    var backgroundColors = [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)',
        'rgba(255, 0, 0)',
        'rgba(0, 255, 0)',
        'rgba(0, 0, 255)',
        'rgba(192, 192, 192)',
        'rgba(255, 255, 0)',
        'rgba(255, 0, 255)'
    ];

    var borderColors = [
        'rgba(255,99,132,1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
        'rgba(255, 0, 0)',
        'rgba(0, 255, 0)',
        'rgba(0, 0, 255)',
        'rgba(192, 192, 192)',
        'rgba(255, 255, 0)',
        'rgba(255, 0, 255)'
    ];

    var params = {
        data: {
            labels: labels,
            datasets: [{
                label: "Drinks Chart",
                backgroundColor: backgroundColors,
                borderColor: borderColors,
                borderWidth: 1,
                data: values
            }]
        },
        options: {
            maintainAspectRatio: false,
            scales: {
            }
        },
        type: 'pie'
    }

    return params;
}