/**
 * Theme Management System
 * Easy to use theme switcher for ASP.NET MVC
 */

// Set theme and save to localStorage
function setTheme(themeName) {
    document.documentElement.setAttribute('data-theme', themeName);
    localStorage.setItem('selectedTheme', themeName);
    
    // Update chart colors if chart exists
    if (typeof updateChartColors === 'function') {
        updateChartColors();
    }
}

// Get current theme
function getCurrentTheme() {
    return document.documentElement.getAttribute('data-theme') || 'gold';
}

// Load theme from localStorage on page load
function loadSavedTheme() {
    const savedTheme = localStorage.getItem('selectedTheme');
    if (savedTheme) {
        setTheme(savedTheme);
    }
}

// Get CSS variable value
function getCSSVariable(variableName) {
    return getComputedStyle(document.documentElement).getPropertyValue(variableName).trim();
}

// Initialize theme on page load
document.addEventListener('DOMContentLoaded', loadSavedTheme);
