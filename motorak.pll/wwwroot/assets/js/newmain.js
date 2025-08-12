// Include all the original main.js content plus enhanced functionality

// Original main.js content here...

// Enhanced functionality for MVC integration
document.addEventListener('DOMContentLoaded', function () {
    // Initialize enhanced features
    initializeEnhancedFeatures();

    // Handle form submissions with loading states
    handleFormSubmissions();

    // Initialize notification system
    initializeNotifications();

    // Handle responsive navigation
    initializeResponsiveNav();
});

function initializeEnhancedFeatures() {
    // Tab switching functionality
    document.querySelectorAll('.dashboard__tab').forEach(tab => {
        tab.addEventListener('click', function () {
            const tabId = this.getAttribute('data-tab');

            // Update active tab
            document.querySelectorAll('.dashboard__tab').forEach(t => t.classList.remove('active'));
            this.classList.add('active');

            // Update active content
            document.querySelectorAll('.dashboard__content').forEach(content => {
                content.classList.remove('active');
            });

            const targetContent = document.getElementById(tabId + '-tab');
            if (targetContent) {
                targetContent.classList.add('active');
            }
        });
    });

    // User dropdown functionality
    const userAvatar = document.getElementById('userAvatar');
    const userDropdown = document.getElementById('userDropdown');

    if (userAvatar && userDropdown) {
        userAvatar.addEventListener('click', function (e) {
            e.stopPropagation();
            userDropdown.classList.toggle('active');
        });

        document.addEventListener('click', function (e) {
            if (!userDropdown.contains(e.target) && !userAvatar.contains(e.target)) {
                userDropdown.classList.remove('active');
            }
        });
    }
}

function handleFormSubmissions() {
    // Add loading states to form submissions
    document.querySelectorAll('form').forEach(form => {
        form.addEventListener('submit', function (e) {
            const submitButton = form.querySelector('button[type="submit"]');
            if (submitButton) {
                const originalText = submitButton.textContent;
                submitButton.innerHTML = '<span class="loading"></span>';
                submitButton.disabled = true;

                // Re-enable after submission (in case of client-side validation failure)
                setTimeout(() => {
                    if (submitButton.disabled) {
                        submitButton.textContent = originalText;
                        submitButton.disabled = false;
                    }
                }, 5000);
            }
        });
    });
}

function initializeNotifications() {
    // Show notifications for TempData messages
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        // Auto-hide success messages after 5 seconds
        if (alert.classList.contains('alert-success')) {
            setTimeout(() => {
                alert.style.opacity = '0';
                setTimeout(() => alert.remove(), 300);
            }, 5000);
        }
    });
}

function initializeResponsiveNav() {
    const navToggle = document.getElementById('nav-toggle');
    const navMenu = document.getElementById('nav-menu');

    if (navToggle && navMenu) {
        navToggle.addEventListener('click', () => {
            navMenu.classList.toggle('show');
        });
    }
}

// Utility function for showing custom notifications
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `alert alert-${type} notification-toast`;
    notification.innerHTML = `<i class="ri-information-line"></i> ${message}`;
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 1000;
        min-width: 300px;
        animation: slideInRight 0.3s ease;
    `;

    document.body.appendChild(notification);

    setTimeout(() => {
        notification.style.animation = 'slideOutRight 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 5000);
}

// Add notification animations to CSS
const notificationStyles = `
    @keyframes slideInRight {
        from { transform: translateX(100%); opacity: 0; }
        to { transform: translateX(0); opacity: 1; }
    }
    @keyframes slideOutRight {
        from { transform: translateX(0); opacity: 1; }
        to { transform: translateX(100%); opacity: 0; }
    }
`;

const styleSheet = document.createElement('style');
styleSheet.textContent = notificationStyles;
document.head.appendChild(styleSheet);