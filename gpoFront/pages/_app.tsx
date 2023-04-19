import "../styles/globals.css";
import type { AppProps } from "next/app";
import Layout from "../components/layout/layout";
import { useRouter } from "next/router";
import { useEffect } from "react";
import ProfileUserContextProvider from "../components/context/profileUserContext";

function MyApp({ Component, pageProps }: AppProps) {
  const router = useRouter();

  useEffect(() => {
    //@ts-ignore
    const authValue = JSON.parse(localStorage.getItem("auth"));
    if (!authValue || authValue === "nologin") {
      router.push("/auth");
    } else if (authValue === "admin") {
      router.push("/adminPanel");
    } else {
      router.push("/profile");
    }
  }, []);

  return (
    <ProfileUserContextProvider>
      <Layout>
        <Component {...pageProps} />
      </Layout>
    </ProfileUserContextProvider>
  );
}

export default MyApp;
