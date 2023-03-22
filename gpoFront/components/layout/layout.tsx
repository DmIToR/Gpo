import Footer from "./footer";
import Header from "./header";

const Layout = ({ children }: any) => {
  return (
    <div className="w-full h-full relative">
      <Header />
        {children}
      <Footer />
    </div>
  );
};

export default Layout;
