import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Recipe } from "../models/Recipe"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async updateProfile(profileData) {
    try {
      const res = await api.put('/account', profileData)
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('Attempted account updates', err)
    }
  }

  async getFavsByAccountId() {
    try {
      const res = await api.get('/account/favorites');
      AppState.favRecipes = res.data.map(r => new Recipe(r));
    }
    catch (err) { logger.error(err) }
  }
}

export const accountService = new AccountService()
