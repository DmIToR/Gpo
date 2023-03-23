/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx}",
    "./components/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'gray-main': '#f5f5f5',
        'blue-active': '#388bff',
        'overlay-gray': 'rgba(13, 12, 12, 0.8)',
      },
    },
  },
  variants: {
    extend: {
      textDecoration: ["active", "focus", 'group-hover'],
      fill: ["hover", "focus", "active", 'group-hover'],
      backgroundColor: ["group-focus", 'group-hover'],
    },
  },
  plugins: [],
}