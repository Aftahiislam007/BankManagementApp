describe("User Authentication and Authorization", () => {
    beforeEach(() => {
        cy.viewport(1024, 720);

        cy.visit("/swagger");
      });

    it("should log in with valid credentials and return a JWT token", () => {
    //   cy.login("customer", "password_123").then(() => {
    //     expect(Cypress.env("token")).to.exist;
    //   });

    cy.request('POST', '/api/auth/login', {
        username: 'Rahat',
        password: 'Rahat123'
      }).then((loginResponse) => {
        const token = loginResponse.body.token;
  
        cy.request({
          method: 'GET',
          url: '/api/protected',
          headers: {
            Authorization: `Bearer ${token}`
          }
        }).then((protectedResponse) => {
          expect(protectedResponse.status).to.equal(200);
        });
      });
    });
  
    it("should fail login with invalid credentials", () => {
      cy.request({
        method: "POST",
        url: "/api/Auth/login",
        body: { username: "invalid", password: "wrong" },
        failOnStatusCode: false,
      }).then((response) => {
        expect(response.status).to.eq(401);
      });
    });
  
    it("should restrict access to protected endpoints without a valid token", () => {
      cy.request({
        method: "GET",
        url: "/api/Account",
        failOnStatusCode: false,
      }).then((response) => {
        expect(response.status).to.eq(401);
      });
    });
  });