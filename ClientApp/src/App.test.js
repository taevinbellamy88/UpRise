import { render, screen } from "@testing-library/react";
import MainHero from "./components/MainHero.jsx";

test("MainHero Heading", () => {
   render(<MainHero />);
   const linkElement = screen.getByText(/Let's Build/i);
   expect(linkElement).toBeInTheDocument();
});
