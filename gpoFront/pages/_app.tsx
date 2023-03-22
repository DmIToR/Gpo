import '../styles/globals.css'
import type { AppProps } from 'next/app'
import Layout from '../components/layout/layout'
import { useRouter } from 'next/router';
import { useEffect } from 'react';

function MyApp({ Component, pageProps }: AppProps) {
  const router = useRouter();

  useEffect(() => {
    if (router.asPath === '/')
      router.push(`/profile`);
  });

  return (
    <Layout>
      <Component {...pageProps} />
    </Layout>
  )
}

export default MyApp
