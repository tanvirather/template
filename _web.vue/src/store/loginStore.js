export class LoginStore {
  #apiClient;
  #url = "account"

  constructor(apiClient) {
    this.#apiClient = apiClient
  }

  async login(model) {
    // alert(model);
    let result = await this.#apiClient.post(this.#url + "/login", model);
    alert(result.token);
    return result;
  }

}
