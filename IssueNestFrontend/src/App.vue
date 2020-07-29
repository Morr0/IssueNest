<template>
    <div id="app">
        <Login v-if="!loggedIn" />
        <button type="button" v-if="loggedIn" @click.prevent="logout">Logout</button>

        <div v-if="loggedIn">
            Projects
            <!-- <Projects /> -->
        </div>
    </div>
</template>

<script>
import Login from "@/views/Login.vue";

export default {
    components: {
        Login
    },
    data(){
        document.state = this.$store.state;
        return {
            currentView: "home",
        };
    },
    computed: {
        user: function (){
            return this.$store.state.user;
        },
        loggedIn: function (){
            if (this.user)
                return this.$store.state.user.id;
            
            return false;
        }
    },
    methods: {
        logout: async function (){
            const res = await fetch("https://localhost:5001/api/auth/logout", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    id: this.user.id,
                }),
                credentials: "include"
            });

            // logout in any case
            this.$store.commit("setUser", undefined)
        }
    }
}
</script>