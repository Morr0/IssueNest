<template>
    <div id="app">
        <Login v-if="!loggedIn" />
        <button type="button" v-if="loggedIn">Logout</button>

        <div v-if="loggedIn">
            Projects
            <Projects />
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
        return {
            currentView: "home",
        };
    },
    computed: {
        loggedIn: function (){
            if (this.$store.state.user)
                return this.$store.state.user.id;
            
            return false;
        }
    }
}
</script>