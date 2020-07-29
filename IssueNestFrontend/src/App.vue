<template>
    <div id="app">
        <Login v-if="!loggedIn" />
        <div v-if="loggedIn">
            {{user.name}} | 
            <button type="buttton" @click.prevent="currentView='Projects'">Projects</button> | 
            <button type="button" @click.prevent="currentView='Integrations'">Integrations</button> | 
            <button type="button" @click.prevent="logout">Logout</button>
            <br>

            <keep-alive>
                <component :is="currentView" :user="user"></component>
            </keep-alive>
            
        </div>
    </div>
</template>

<script>
import Login from "@/views/Login.vue";
import Projects from "@/views/Projects.vue";
import Integrations from "@/views/Integrations.vue";

export default {
    components: {
        Login,
        Projects,
        Integrations
    },
    data(){
        document.state = this.$store.state;
        return {
            currentView: "Projects",
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
        },
    }
}
</script>