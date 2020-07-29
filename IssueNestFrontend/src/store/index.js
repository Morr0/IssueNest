import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
      user: undefined,
  },
  mutations: {
      setUser(state, user){
          state.user = user;
      }
  },
  actions: {
  },
  modules: {
  },
  plugins: [
      createPersistedState({
        storage: window.sessionStorage,
      })
  ]
})
