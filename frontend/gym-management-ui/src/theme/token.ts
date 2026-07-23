/**
 * FitTrace design tokens — single source of truth for color, type, spacing, radii,
 * shadows and motion. Mirrors tokens/*.css. Consume raw here, or via antdToken.ts.
 */

/* ---------------------------------------------------------------- palette */
export const palette = {
  // Neutrals — slate / charcoal
  slate950: '#080A0F', slate900: '#0B0E14', slate850: '#0F131C', slate800: '#12151E',
  slate750: '#171B26', slate700: '#1F2431', slate650: '#262C3A', slate600: '#333A4D',
  slate500: '#4A5468', slate400: '#6B7488', slate300: '#8D95A8', slate200: '#B4BAC8',
  slate150: '#D0D4DE', slate100: '#E2E5EC', slate50: '#F2F3F6', white: '#FFFFFF',

  // Brand accent — energy orange
  orange700: '#C23D0B', orange600: '#E5551A', orange500: '#FF6A2B', orange400: '#FF8552',
  orange300: '#FFA47D', orange200: '#FFC6AC', orange100: '#FFE4D6',

  // Secondary energy — electric lime
  lime500: '#A9E635', lime400: '#C2F14E', lime300: '#D6F781',

  // Semantic hues
  green500: '#35D07F', red500: '#FF5C5C', amber500: '#FFB43D', blue500: '#4B9BFF',

  // Data-viz ramp
  vizOrange: '#FF6A2B', vizLime: '#A9E635', vizCyan: '#35D0C8',
  vizViolet: '#8B7BFF', vizPink: '#FF6FB5', vizBlue: '#4B9BFF',
} as const;

/* ------------------------------------------------------- semantic themes */
export const semantic = {
  dark: {
    bg: '#0B0E14', bgSubtle: '#080A0F', surface: '#12151E', surface2: '#171B26',
    surface3: '#1F2431', surfaceInset: '#0F131C',
    border: '#232936', borderStrong: '#333A4D', divider: '#1A1F2B',
    text: '#F2F3F6', textMuted: '#9AA2B4', textSubtle: '#6B7488', textInverse: '#0B0E14',
    accent: '#FF6A2B', accentHover: '#FF8552', accentActive: '#E5551A', accentFg: '#0B0E14',
    accentSoft: 'rgba(255,106,43,0.13)', accentRing: 'rgba(255,106,43,0.45)',
    success: '#35D07F', successSoft: 'rgba(53,208,127,0.14)',
    danger: '#FF5C5C', dangerSoft: 'rgba(255,92,92,0.14)',
    warning: '#FFB43D', warningSoft: 'rgba(255,180,61,0.14)',
    info: '#4B9BFF', infoSoft: 'rgba(75,155,255,0.14)',
    overlay: 'rgba(6,8,13,0.68)',
  },
  light: {
    bg: '#F4F5F8', bgSubtle: '#FFFFFF', surface: '#FFFFFF', surface2: '#F5F6F9',
    surface3: '#ECEEF3', surfaceInset: '#F0F1F5',
    border: '#E2E5EC', borderStrong: '#CDD2DC', divider: '#EDEFF3',
    text: '#12151E', textMuted: '#5A6478', textSubtle: '#8D95A8', textInverse: '#FFFFFF',
    accent: '#F2560E', accentHover: '#FF6A2B', accentActive: '#D6480A', accentFg: '#FFFFFF',
    accentSoft: 'rgba(242,86,14,0.10)', accentRing: 'rgba(242,86,14,0.35)',
    success: '#17A85A', successSoft: 'rgba(23,168,90,0.12)',
    danger: '#E23B3B', dangerSoft: 'rgba(226,59,59,0.10)',
    warning: '#C97A00', warningSoft: 'rgba(201,122,0,0.12)',
    info: '#2A7DE1', infoSoft: 'rgba(42,125,225,0.12)',
    overlay: 'rgba(18,21,30,0.42)',
  },
} as const;

/* ---------------------------------------------------------- typography */
export const typography = {
  fontDisplay: "'Space Grotesk', 'Manrope', system-ui, sans-serif",
  fontSans: "'Manrope', system-ui, -apple-system, sans-serif",
  fontMono: "'JetBrains Mono', 'SF Mono', ui-monospace, monospace",
  fontMetric: "'Space Grotesk', 'Manrope', sans-serif",
  size: { '2xs': 11, xs: 12, sm: 13, md: 15, lg: 17, xl: 20, '2xl': 25, '3xl': 32, '4xl': 42, '5xl': 56, '6xl': 72 },
  weight: { regular: 400, medium: 500, semibold: 600, bold: 700, extra: 800 },
  leading: { tight: 1.08, snug: 1.24, normal: 1.5, relaxed: 1.65 },
  tracking: { tight: '-0.02em', normal: '0', wide: '0.02em', caps: '0.08em' },
} as const;

/* ------------------------------------------------------------- spacing */
export const spacing = {
  0: 0, 1: 4, 2: 8, 3: 12, 4: 16, 5: 20, 6: 24, 8: 32, 10: 40, 12: 48, 16: 64, 20: 80, 24: 96,
} as const;

/* -------------------------------------------------------------- radii */
export const radius = { xs: 4, sm: 6, md: 8, lg: 12, xl: 16, '2xl': 22, pill: 999 } as const;

/* ------------------------------------------------------------ shadows */
export const shadow = {
  dark: {
    xs: '0 1px 2px rgba(0,0,0,0.35)', sm: '0 2px 6px rgba(0,0,0,0.35)',
    md: '0 8px 24px rgba(0,0,0,0.40)', lg: '0 18px 48px rgba(0,0,0,0.50)',
    xl: '0 30px 80px rgba(0,0,0,0.55)',
    glowAccent: '0 6px 20px rgba(255,106,43,0.35)',
    glowAccentStrong: '0 8px 32px rgba(255,106,43,0.50)',
  },
  light: {
    xs: '0 1px 2px rgba(18,21,30,0.06)', sm: '0 2px 6px rgba(18,21,30,0.08)',
    md: '0 8px 24px rgba(18,21,30,0.10)', lg: '0 18px 48px rgba(18,21,30,0.14)',
    xl: '0 30px 80px rgba(18,21,30,0.16)',
    glowAccent: '0 6px 20px rgba(242,86,14,0.28)',
    glowAccentStrong: '0 8px 32px rgba(242,86,14,0.38)',
  },
} as const;

/* -------------------------------------------------------------- motion */
export const motion = {
  ease: {
    standard: 'cubic-bezier(0.4, 0, 0.2, 1)',
    out: 'cubic-bezier(0.16, 1, 0.3, 1)',
    spring: 'cubic-bezier(0.34, 1.56, 0.64, 1)',
  },
  duration: { fast: 120, base: 180, slow: 280, slower: 420 },
} as const;

/* -------------------------------------------------------------- z-index */
export const z = { base: 0, sticky: 100, dropdown: 400, overlay: 800, modal: 900, toast: 1000 } as const;

export type ThemeMode = keyof typeof semantic;
export type Semantic = (typeof semantic)['dark'];

const tokens = { palette, semantic, typography, spacing, radius, shadow, motion, z };
export default tokens;
