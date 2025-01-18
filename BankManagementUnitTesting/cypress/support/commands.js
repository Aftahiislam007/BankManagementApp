// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add('login', (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add('drag', { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add('dismiss', { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite('visit', (originalFn, url, options) => { ... })



Cypress.Commands.add("login", (username, password) => {
    cy.request({
      method: "POST",
      url: "/api/Auth/login",
      body: { username, password },
    }).then((response) => {
      expect(response.status).to.eq(200);
      Cypress.env("token", response.body.token); 
    });
  });
  
  Cypress.Commands.add("createAccount", (accountData) => {
    cy.request({
      method: "POST",
      url: "/api/Account",
      headers: { Authorization: `Bearer ${Cypress.env("token")}` },
      body: accountData,
    }).then((response) => {
      expect(response.status).to.eq(201);
      return response.body;
    });
  });
  
  Cypress.Commands.add("getAccountDetails", (accountId) => {
    cy.request({
      method: "GET",
      url: `/api/Account/${accountId}`,
      headers: { Authorization: `Bearer ${Cypress.env("token")}` },
    }).then((response) => {
      expect(response.status).to.eq(200);
      return response.body;
    });
  });
  
  Cypress.Commands.add("transferFunds", (transferData) => {
    cy.request({
      method: "POST",
      url: "/transfer",
      headers: { Authorization: `Bearer ${Cypress.env("token")}` },
      body: transferData,
    }).then((response) => {
      expect(response.status).to.eq(200);
    });
  });