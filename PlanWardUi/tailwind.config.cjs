const colors = require('tailwindcss/colors')

module.exports = {
  content: [
    "./index.html",
    "./src/**/*.{svelte,js,ts,jsx,tsx}",
  ],
  theme: {
    fontFamily: {
      'title': ['Tahoma', 'Verdana', 'sans-serif'],
      'main': ['Georgia', 'serif'],
      'accent': ['Georgia', 'serif']
    },
    colors: {
      transparent: "transparent",
      current: "currentColor",
      "white": "var(--white)",
      gray: colors.gray,
      red: colors.red,
      orange: colors.amber,
      green: colors.green,
    },
    screens: {
      'base': '390px',
      'lg': '650px',
    },
    extend: {
      colors: {
        "pw-green": "var(--pw-green)",
        "pw-yellow": "var(--pw-yellow)",
        "pw-blue": "var(--pw-blue)",
        "pw-navy": "var(--pw-navy)",
        "pw-red": "var(--pw-red)"
      },
    },
  },
  plugins: [],
}
