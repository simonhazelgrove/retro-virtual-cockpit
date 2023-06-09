import f16cp_st_am from './f16cp_st_am'
import f19sf_st_am from './f19sf_st_am'
import combat_lynx_spectrum from './combat_lynx_spectrum'

export default function cockpit_configs() {
    return [
        f16cp_st_am(),
        f19sf_st_am(),
        combat_lynx_spectrum()
    ]
}
