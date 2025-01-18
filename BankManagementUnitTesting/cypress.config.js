const { defineConfig } = require("cypress");

module.exports = defineConfig({
  e2e: {
    setupNodeEvents(on, config) {
      config["baseUrl"] = "https://localhost:7292";
      return config;
    },
  },
});
