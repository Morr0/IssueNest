import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
      userId: undefined,
  },
  mutations: {
      setUser(state, userId){
          state.userId = userId;
      }
  },
  actions: {
  },
  modules: {
  }
})
