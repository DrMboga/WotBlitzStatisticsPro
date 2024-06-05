var chartsHelper = chartsHelper || {};

chartsHelper.BuildHistoryChart = function (elementId, label, data) {
  const ctx = document.getElementById(elementId).getContext('2d');
  const myChart = new Chart(ctx, {
    type: 'line',
    data: data,
    options: {
      maintainAspectRatio: false,
      responsive: true,
      plugins: {
        legend: {
          display: false,
        },
        title: {
          display: true,
          text: label,
        },
      },
      elements: {
        point: {
          radius: 0,
        },
      },
      scales: {
        x: {
          display: true,
          title: {
            display: false,
          },
          grid: {
            color: '#3d3b3b',
            lineWidth: 0.5,
          },
        },
        y: {
          display: true,
          title: {
            display: false,
          },
          grid: {
            color: '#3d3b3b',
            lineWidth: 0.5,
          },
        },
      },
    },
  });

  return myChart;
};

chartsHelper.addData = function (chart, newData) {
  chart.data = newData;
  chart.update();
};

chartsHelper.removeData = function (chart) {
  chart.data.labels = [];
  chart.data.datasets = [];
  chart.update();
};
