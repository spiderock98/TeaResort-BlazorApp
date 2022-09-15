let myChart;

function DayChart() {
    const ctx = document.getElementById('myChart').getContext('2d');

    const data = {
        labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
        datasets: [
            {
                label: '1 of Votes',
                type: 'bar',
                data: [12, 19, 23, 5, 12, 23],
                backgroundColor:
                    'rgba(255, 99, 132, 0.2)',

                //,
                //borderColor: [
                //    'rgba(255, 159, 64, 1)'
                //],
                //borderWidth: 1
            },
            {
                label: '1 of Votes',
                type: 'bar',
                data: [12, 9, 13, 15, 22, 23],
                backgroundColor:
                    'rgba(255, 0, 132, 0.2)',
                //,
                //borderColor: [

                //    'rgba(255, 159, 64, 1)'
                //],
                //borderWidth: 1
            },
            {
                type: 'line',
                label: 'Line Dataset',
                data: [15, 20, 30, 15, 30, 20],
                fill: false,
                borderColor: 'rgb(54, 162, 235)'
            }
        ]
    };

    const options = {
        scales: {
            y: {
                beginAtZero: true,
                stacked: true
            },
            x: {
                stacked: true
            }
        },
        responsive: true,
        maintainAspectRatio: false
    }

    const config = {
        type: 'bar',
        data: data,
        options: options
    }

    myChart = new Chart(ctx, config);
};

function RenderChart() {
    myChart.destroy();
}