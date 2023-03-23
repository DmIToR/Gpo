import '../styles/globals.css'
import type { AppProps } from 'next/app'
import Layout from '../components/layout/layout'
import { useRouter } from 'next/router';
import { useEffect } from 'react';

function MyApp({ Component, pageProps }: AppProps) {
  const router = useRouter();

  useEffect(() => { //@ts-ignore
    var tempObject = JSON.parse(localStorage.getItem("auth"));
    if(localStorage.getItem("auth") === null || tempObject === 'nologin') {
      router.push(`/auth`);
    } else router.push(`/profile`)
  },[router]);


  return (
    <Layout>
      <Component {...pageProps} />
    </Layout>
  )
}

export default MyApp
