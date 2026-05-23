/**
 * Main JavaScript File
 * Currency Exchange Website - ASP.NET MVC
 */

document.addEventListener('DOMContentLoaded', function() {
    // Initialize all components
    initNavbar();
    initCounters();
    initChart();
    initSmoothScroll();
    simulatePriceUpdates();
});

/**
 * Navbar scroll effect
 */
function initNavbar() {
    const navbar = document.querySelector('.navbar-custom');
    
    window.addEventListener('scroll', function() {
        if (window.scrollY > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
    });
}

/**
 * Animated counters in hero section
 */
function initCounters() {
    const counters = document.querySelectorAll('.stat-number');
    
    const observerOptions = {
        threshold: 0.5
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const counter = entry.target;
                animateCounter(counter);
                observer.unobserve(counter);
            }
        });
    }, observerOptions);
    
    counters.forEach(counter => observer.observe(counter));
}

function animateCounter(element) {
    const target = parseInt(element.getAttribute('data-count'));
    const duration = 2000;
    const step = target / (duration / 16);
    let current = 0;
    
    const timer = setInterval(function() {
        current += step;
        if (current >= target) {
            element.textContent = formatNumber(target);
            clearInterval(timer);
        } else {
            element.textContent = formatNumber(Math.floor(current));
        }
    }, 16);
}

function formatNumber(num) {
    if (num >= 1000000) {
        return (num / 1000000).toFixed(1) + 'M+';
    } else if (num >= 1000) {
        return (num / 1000).toFixed(0) + 'K+';
    }
    return num.toLocaleString('fa-IR');
}

/**
 * Exchange Rate Chart
 */
let rateChart = null;

function initChart() {
    const ctx = document.getElementById('rateChart');
    if (!ctx) return;
    
    const chartData = generateChartData('week');
    
    rateChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: chartData.labels,
            datasets: [{
                label: 'دلار آمریکا (تومان)',
                data: chartData.values,
                borderColor: getCSSVariable('--primary'),
                backgroundColor: createGradient(ctx),
                borderWidth: 3,
                fill: true,
                tension: 0.4,
                pointRadius: 4,
                pointBackgroundColor: getCSSVariable('--primary'),
                pointBorderColor: '#fff',
                pointBorderWidth: 2,
                pointHoverRadius: 6
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: true,
                    position: 'top',
                    labels: {
                        color: getCSSVariable('--text-secondary'),
                        font: {
                            family: 'Vazirmatn'
                        }
                    }
                },
                tooltip: {
                    backgroundColor: getCSSVariable('--background-card'),
                    titleColor: getCSSVariable('--text-primary'),
                    bodyColor: getCSSVariable('--text-secondary'),
                    borderColor: getCSSVariable('--border-color'),
                    borderWidth: 1,
                    cornerRadius: 10,
                    padding: 15,
                    displayColors: false,
                    callbacks: {
                        label: function(context) {
                            return context.parsed.y.toLocaleString('fa-IR') + ' تومان';
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: {
                        color: getCSSVariable('--border-color'),
                        drawBorder: false
                    },
                    ticks: {
                        color: getCSSVariable('--text-muted'),
                        font: {
                            family: 'Vazirmatn'
                        }
                    }
                },
                y: {
                    grid: {
                        color: getCSSVariable('--border-color'),
                        drawBorder: false
                    },
                    ticks: {
                        color: getCSSVariable('--text-muted'),
                        font: {
                            family: 'Vazirmatn'
                        },
                        callback: function(value) {
                            return value.toLocaleString('fa-IR');
                        }
                    }
                }
            },
            interaction: {
                intersect: false,
                mode: 'index'
            }
        }
    });
    
    // Chart period buttons
    const chartBtns = document.querySelectorAll('.chart-btn');
    chartBtns.forEach(btn => {
        btn.addEventListener('click', function() {
            chartBtns.forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            
            const period = this.getAttribute('data-period');
            updateChart(period);
        });
    });
}

function createGradient(ctx) {
    const gradient = ctx.getContext('2d').createLinearGradient(0, 0, 0, 300);
    const primaryColor = getCSSVariable('--primary-rgb') || '212, 175, 55';
    gradient.addColorStop(0, `rgba(${primaryColor}, 0.3)`);
    gradient.addColorStop(1, `rgba(${primaryColor}, 0)`);
    return gradient;
}

function generateChartData(period) {
    const labels = [];
    const values = [];
    let days;
    
    switch(period) {
        case 'week':
            days = 7;
            break;
        case 'month':
            days = 30;
            break;
        case 'year':
            days = 12;
            break;
        default:
            days = 7;
    }
    
    const persianDays = ['شنبه', 'یکشنبه', 'دوشنبه', 'سه‌شنبه', 'چهارشنبه', 'پنجشنبه', 'جمعه'];
    const persianMonths = ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'];
    
    const basePrice = 58000;
    
    for (let i = 0; i < days; i++) {
        if (period === 'week') {
            labels.push(persianDays[i % 7]);
        } else if (period === 'month') {
            labels.push((i + 1).toLocaleString('fa-IR'));
        } else {
            labels.push(persianMonths[i]);
        }
        
        // Generate random price fluctuation
        const fluctuation = (Math.random() - 0.5) * 2000;
        const trend = i * (Math.random() * 100);
        values.push(Math.round(basePrice + fluctuation + trend));
    }
    
    return { labels, values };
}

function updateChart(period) {
    if (!rateChart) return;
    
    const newData = generateChartData(period);
    rateChart.data.labels = newData.labels;
    rateChart.data.datasets[0].data = newData.values;
    rateChart.update('active');
}

function updateChartColors() {
    if (!rateChart) return;
    
    const ctx = document.getElementById('rateChart');
    rateChart.data.datasets[0].borderColor = getCSSVariable('--primary');
    rateChart.data.datasets[0].backgroundColor = createGradient(ctx);
    rateChart.data.datasets[0].pointBackgroundColor = getCSSVariable('--primary');
    rateChart.options.plugins.legend.labels.color = getCSSVariable('--text-secondary');
    rateChart.options.scales.x.ticks.color = getCSSVariable('--text-muted');
    rateChart.options.scales.y.ticks.color = getCSSVariable('--text-muted');
    rateChart.options.scales.x.grid.color = getCSSVariable('--border-color');
    rateChart.options.scales.y.grid.color = getCSSVariable('--border-color');
    rateChart.update();
}

function getCSSVariable(name) {
    return getComputedStyle(document.documentElement).getPropertyValue(name).trim();
}

/**
 * Smooth scroll for anchor links
 */
function initSmoothScroll() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                const navbarHeight = document.querySelector('.navbar-custom').offsetHeight;
                const targetPosition = target.offsetTop - navbarHeight;
                
                window.scrollTo({
                    top: targetPosition,
                    behavior: 'smooth'
                });
                
                // Close mobile menu if open
                const navbarCollapse = document.querySelector('.navbar-collapse');
                if (navbarCollapse.classList.contains('show')) {
                    const bsCollapse = bootstrap.Collapse.getInstance(navbarCollapse);
                    if (bsCollapse) bsCollapse.hide();
                }
            }
        });
    });
}

/**
 * Simulate real-time price updates
 */
function simulatePriceUpdates() {
    setInterval(function() {
        updateRandomPrice();
    }, 5000);
}

function updateRandomPrice() {
    const tables = document.querySelectorAll('.rate-table tbody');
    
    tables.forEach(table => {
        const rows = table.querySelectorAll('tr');
        if (rows.length === 0) return;
        
        const randomRow = rows[Math.floor(Math.random() * rows.length)];
        const priceCells = randomRow.querySelectorAll('.price-buy, .price-sell');
        
        priceCells.forEach(cell => {
            // Flash animation
            cell.style.transition = 'background-color 0.3s ease';
            cell.style.backgroundColor = 'rgba(var(--primary-rgb), 0.2)';
            
            setTimeout(() => {
                cell.style.backgroundColor = 'transparent';
            }, 500);
        });
    });
}

/**
 * Format Persian numbers
 */
function toPersianNumber(num) {
    const persianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    return num.toString().replace(/\d/g, d => persianDigits[d]);
}

/**
 * Add comma to numbers
 */
function addCommas(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

/**
 * AJAX function for real-time rate updates (can be connected to API)
 */
function fetchRatesFromServer() {
    // Example: Connect to your ASP.NET API
    fetch('/api/rates')
        .then(response => response.json())
        .then(data => {
            // Update rates in the UI
            updateCurrencyRates(data.currencyRates);
            updateCoinRates(data.coinRates);
            // ... etc
        })
        .catch(error => console.error('Error fetching rates:', error));
}

function updateCurrencyRates(rates) {
    const tbody = document.getElementById('currency-rates');
    if (!tbody || !rates) return;
    
    // Update each row with new data
    rates.forEach((rate, index) => {
        const row = tbody.rows[index];
        if (row) {
            const buyCell = row.querySelector('.price-buy');
            const sellCell = row.querySelector('.price-sell');
            if (buyCell) buyCell.textContent = toPersianNumber(addCommas(rate.buyPrice));
            if (sellCell) sellCell.textContent = toPersianNumber(addCommas(rate.sellPrice));
        }
    });
}
