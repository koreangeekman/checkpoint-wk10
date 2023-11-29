export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'https://localhost:7045' : ''
export const useSockets = false
export const domain = 'dev-koreangeekman-lab.us.auth0.com'
export const clientId = '1MYHxJKnh3bxFiznLeQQ8oJKisH5OJpD'
export const audience = 'https://geekman.lab0'