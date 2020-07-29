<template>
    <form>
        <label for="email">Email: </label>
        <input type="email" name="email" placeholder="example@example.example" v-model="email" required />
        <label for="password">Password: </label>
        <input type="password" placeholder="********" v-model="password" required />
        <button type="submit" @click.prevent="login">Login</button>
    </form>
</template>

<script>
export default {
    data(){
        return {
            email: "",
            password: "",
        };
    },
    methods: {
        login: async function (){
            console.log("Logging in");

            // TODO validate
            const res = await fetch(`https://localhost:5001/api/auth/login`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Access-Control-Allow-Origin": "Access-Control-Allow-Origin"
                },
                body: JSON.stringify({
                    email: this.email,
                    password: this.password
                }),
                credentials: "include"
            });
            if (res.status === 200){
                const data = await res.json();
                return this.$store.commit("setUser", data.user);
            }

            alert("Please enter the correct credentials");
        }
    },
    computed: {
        user: function (){
            return this.$store.state.user;
        }
    }
}
</script>

<style>

</style>